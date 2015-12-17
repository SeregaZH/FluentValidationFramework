using System;

namespace FluentValidation.Validation.Models
{
    /// <summary>
    /// String validator options.
    /// Is used to configure string required validator.
    /// </summary>
    public sealed class StringValidatorOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringValidatorOptions"/> class.
        /// </summary>
        /// <param name="isTrimmed">if set to <c>true</c> spaces leading and trailing spaces must be trimmed.</param>
        /// <param name="comparationType">Type of the string comparation.</param>
        public StringValidatorOptions(bool isTrimmed, StringComparison comparationType)
        {
            IsTrimmed = isTrimmed;
            ComparationType = comparationType;
        }

        /// <summary>
        /// Gets a value indicating whether validated string is trimmed.
        /// </summary>
        /// <value>
        /// <c>true</c> if leading and trailing spaces must be trimmed; otherwise, <c>false</c> string is not changed.
        /// </value>
        public bool IsTrimmed { get; }

        /// <summary>
        /// Gets the type of the string comparation type.
        /// </summary>
        /// <value>
        /// The type of the string comparation.
        /// </value>
        public StringComparison ComparationType { get; }
    }
}
