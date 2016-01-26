using System;

namespace FluentValidationFramework.Validation.Models.Options
{
    /// <summary>
    /// String validator options.
    /// Is used to configure string required validator.
    /// </summary>
    public sealed class StringValidatorOptions : BaseStringValidationOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringValidatorOptions"/> class.
        /// </summary>
        /// <param name="isTrimmed">if set to <c>true</c> spaces leading and trailing spaces must be trimmed (nested <see cref="BaseStringValidationOptions" />).</param>
        /// <param name="comparationType">Type of the string comparation.</param>
        public StringValidatorOptions(bool isTrimmed, StringComparison comparationType)
            : base(isTrimmed)
        {
            ComparationType = comparationType;
        }

        /// <summary>
        /// Gets the type of the string comparation type.
        /// </summary>
        /// <value>
        /// The type of the string comparation.
        /// </value>
        public StringComparison ComparationType { get; private set; }
    }
}
