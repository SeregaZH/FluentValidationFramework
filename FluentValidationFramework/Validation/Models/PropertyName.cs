namespace FluentValidationFramework.Validation.Models
{
    public sealed class PropertyName
    {
        public PropertyName(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
