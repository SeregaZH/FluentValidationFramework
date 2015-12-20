namespace FluentValidation.Validation.Models.Results
{
    /// <summary>
    /// The validation result.
    /// </summary>
    public interface IValidationResult
    {
        /// <summary>
        /// Returns <c>true</c> if validator is valid, otherwise <c>false</c>.
        /// </summary>
        /// <returns><see cref="bool"/>Validation result.</returns>
        bool IsValid();
    }
}
