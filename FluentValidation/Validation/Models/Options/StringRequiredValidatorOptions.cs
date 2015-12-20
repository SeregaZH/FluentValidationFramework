using System;

namespace FluentValidation.Validation.Models.Options
{
    /// <summary>
    /// Validation options for string required validator.
    /// </summary>
    /// <seealso cref="BaseStringValidationOptions" />
    public sealed class StringRequiredValidatorOptions : BaseStringValidationOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringRequiredValidatorOptions"/> class.
        /// </summary>
        /// <param name="isTrimmed">if set to <c>true</c> spaces leading and trailing spaces must be trimmed (nested <see cref="BaseStringValidationOptions" />).</param>
        /// <param name="comparationType">Type of the string comparation.</param>
        public StringRequiredValidatorOptions(bool isTrimmed, StringComparison comparationType)
            : base(isTrimmed)
        {
            ComparationType = comparationType;
        }

        /// <summary>
        /// Gets the type of the comparation.
        /// </summary>
        /// <value>
        /// The type of the string comparation.
        /// </value>
        public StringComparison ComparationType { get; }
    }
}
