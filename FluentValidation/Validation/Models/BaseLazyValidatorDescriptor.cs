using System;

namespace FluentValidation.Validation.Models
{
    public class BaseLazyValidatorDescriptor<TResolver> : BaseValidatorDescriptor
        where TResolver: class
    {
        public BaseLazyValidatorDescriptor(
            Guid id, 
            string key,
            TResolver errorMessageResolver,
            TResolver descriptionResolver)
            : base(id, key)
        {
            ErrorMessageResolver = errorMessageResolver;
            DescriptionResolver = descriptionResolver;
        }

        public TResolver ErrorMessageResolver { get; private set; }

        public TResolver DescriptionResolver { get; private set; }
    }
}
