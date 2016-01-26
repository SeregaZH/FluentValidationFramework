namespace FluentValidationFramework.Validation.Models
{
    public sealed class ValidationValue
    {
        public ValidationValue(object value)
        {
            Value = value;
        }

        public object Value { get; private set; }
    }
}
