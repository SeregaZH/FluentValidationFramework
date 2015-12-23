using System;

namespace FluentValidation.Helpers
{
    internal static class Guard
    {
        internal static void ArgumentNull<T>(T argument, string paramName)
            where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
