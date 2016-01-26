namespace FluentValidationFramework.Validation.Models.Options
{
    /// <summary>
    /// Validation options which are common for all string validators.
    /// </summary>
    public class BaseStringValidationOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseStringValidationOptions"/> class.
        /// </summary>
        /// <param name="isTrimmed">if set to <c>true</c> spaces leading and trailing spaces must be trimmed.</param>
        public BaseStringValidationOptions(bool isTrimmed)
        {
            IsTrimmed = isTrimmed;
        }

        /// <summary>
        /// Gets a value indicating whether validated string is trimmed.
        /// </summary>
        /// <value>
        /// <c>true</c> if leading and trailing spaces must be trimmed; otherwise, <c>false</c> string is not changed.
        /// </value>
        public bool IsTrimmed { get; private set; }
    }
}
