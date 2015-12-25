using System;

namespace FluentValidation.Helpers
{
    /// <summary>
    /// The parameters guard.
    /// </summary>
    internal static class Guard
    {
        /// <summary>
        /// Check argument on null.
        /// </summary>
        /// <typeparam name="T">The argument type.</typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thows if argument is null.</exception>
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
