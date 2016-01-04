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
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="ArgumentNullException">Thows if argument is null.</exception>
        internal static void ArgumentNull<T>(T argument, string argumentName)
            where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Check string argument on null or empty.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="ArgumentNullException">Thows if argument is null or empty.</exception>
        internal static void ArgumentNullOrEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Check string argument on null empty or white space.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="System.ArgumentException">Thows if argument is null, empty or white space.</exception>
        internal static void ArgumentNullEmptyOrWhiteSpace(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument) || string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException(argumentName);
            }
        }
    }
}
