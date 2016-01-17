using LazyPropValidationDescriptor = FluentValidation.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidation.Validation.Models.PropertyName, string>>;
using LazyValValidationDescriptor = FluentValidation.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidation.Validation.Models.PropertyName, FluentValidation.Validation.Models.ValidationValue, string>>;

namespace FluentValidation.Validation.Models
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
