using System;
using System.Linq.Expressions;
using FluentValidationFramework.Validation.Models;
using FluentValidationFramework.Helpers;

namespace FluentValidationFramework.Validation.Validators
{
    public abstract class AsyncPropertyValidator<TModel, TValue> : PropertyValidator<TModel, TValue>
    {
        public AsyncPropertyValidator(Expression<Func<TModel, TValue>> propertyGetter):
            base(propertyGetter)
        {
        }

        protected sealed override PropertyValidationResult ValidateProperty(TValue value, TModel context, string propertyName)
        {
            using (var syncWaiter = AsyncHelper.Sync())
            {
                return syncWaiter.RunSync(() => ValidatePropertyAsync(value, context, propertyName));
            }
        }
    }
}
