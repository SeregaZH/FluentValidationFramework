using LazyPropValidationDescriptor = FluentValidationFramework.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidationFramework.Validation.Models.PropertyName, string>>;
using LazyValValidationDescriptor = FluentValidationFramework.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidationFramework.Validation.Models.PropertyName, FluentValidationFramework.Validation.Models.ValidationValue, string>>;

namespace FluentValidationFramework.Validation.Models
{
    public static class DescriptorResolver
    {
        public static ValidatorDescriptor Resolve(string propertyName, LazyPropValidationDescriptor sourceLazyDescriptor)
        {
            var descriptor = sourceLazyDescriptor.DescriptionResolver(new PropertyName(propertyName));
            var errorMessage = sourceLazyDescriptor.ErrorMessageResolver(new PropertyName(propertyName));
            return new ValidatorDescriptor(sourceLazyDescriptor.Id, sourceLazyDescriptor.Key, errorMessage, descriptor);
        }

        public static ValidatorDescriptor Resolve<TValue>(string propertyName, TValue value, LazyValValidationDescriptor sourceLazyDescriptor)
        {
            var descriptor = sourceLazyDescriptor.DescriptionResolver(new PropertyName(propertyName), new ValidationValue(value));
            var errorMessage = sourceLazyDescriptor.ErrorMessageResolver(new PropertyName(propertyName), new ValidationValue(value));
            return new ValidatorDescriptor(sourceLazyDescriptor.Id, sourceLazyDescriptor.Key, errorMessage, descriptor);
        }
    }
}
