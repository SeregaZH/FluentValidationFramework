﻿using System;
using System.Threading.Tasks;
using EventTask = System.Tuple<System.Threading.SendOrPostCallback, object>;
using EventQueue = System.Collections.Concurrent.ConcurrentQueue<System.Tuple<System.Threading.SendOrPostCallback, object>>;
using System.Threading;

namespace FluentValidationFramework.Helpers
{
    /// <summary>
    /// References: 
    /// 1. http://stackoverflow.com/questions/5095183/how-would-i-run-an-async-taskt-method-synchronously
    /// 2. https://github.com/tejacques/AsyncBridge
    /// </summary>
    public static class AsyncHelper
    {
        public class AsyncBridge : IDisposable
        {
            private readonly ExclusiveSynchronizationContext _currentContext;
            private SynchronizationContext _oldContext;

            internal AsyncBridge()
            {
                _oldContext = SynchronizationContext.Current;
                _currentContext =
                    new ExclusiveSynchronizationContext(_oldContext);
                SynchronizationContext
                    .SetSynchronizationContext(_currentContext);
            }

            public T RunSync<T>(Func<Task<T>> task)
            {
                var result = default(T);
                _currentContext.Post(async _ =>
                {
                    try
                    {
                        var cur = SynchronizationContext.Current;
                        result = await task();
                    }
                    catch (Exception e)
                    {
                        _currentContext.InnerException = e;
                        throw;
                    }
                    finally
                    {
                        _currentContext.EndSyncMessageLoop();
                    }
                }, null);
                _currentContext.BeginSyncMessageLoop();

                return result;
            }

            public void Dispose()
            {
                try
                {
                    SynchronizationContext
                        .SetSynchronizationContext(_oldContext);
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    SynchronizationContext
                        .SetSynchronizationContext(_oldContext);
                }
            }
        }

        public static AsyncBridge Sync()
        {
            return new AsyncBridge();
        }

        private class ExclusiveSynchronizationContext : SynchronizationContext
        {
            private readonly AutoResetEvent _workItemsWaiting =
                new AutoResetEvent(false);

            private bool _doneSync;
            private readonly EventQueue _items;

            public Exception InnerException { get; set; }

            public ExclusiveSynchronizationContext(SynchronizationContext old)
            {
                ExclusiveSynchronizationContext oldEx =
                    old as ExclusiveSynchronizationContext;

                if (null != oldEx)
                {
                    this._items = oldEx._items;
                }
                else
                {
                    this._items = new EventQueue();
                }
            }

            public override void Send(SendOrPostCallback d, object state)
            {
                throw new NotSupportedException(
                    "We cannot send to our same thread");
            }

            public override void Post(SendOrPostCallback d, object state)
            {
                _items.Enqueue(Tuple.Create(d, state));
                _workItemsWaiting.Set();
            }

            public void EndSyncMessageLoop()
            {
                _doneSync = true;
            }

            public void BeginSyncMessageLoop()
            {
                while (!_doneSync)
                {
                    EventTask task = null;

                    if (!_items.TryDequeue(out task))
                    {
                        task = null;
                    }

                    if (task != null)
                    {
                        task.Item1(task.Item2);
                        if (InnerException != null)
                        {
                            throw new AggregateException(
                                "AsyncBridge.Run method threw an exception.",
                                InnerException);
                        }
                    }
                    else
                    {
                        _workItemsWaiting.WaitOne();
                    }
                }
                _doneSync = false;
            }

            public override SynchronizationContext CreateCopy()
            {
                return this;
            }
        }
    }
}

