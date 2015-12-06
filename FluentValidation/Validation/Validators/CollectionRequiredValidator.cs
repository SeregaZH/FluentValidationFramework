using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation.Validators
{
    public sealed class CollectionRequiredValidator<TModel, TCollection> : RequiredValidator<TModel, IEnumerable<TCollection>>
    {
        public CollectionRequiredValidator(ValidatorDescriptor descriptor, int priority, Expression<Func<TModel, IEnumerable<TCollection>>> propertyGetter) 
            : base(descriptor, priority, propertyGetter)
        {
        }

        protected override PropertyValidationResult ValidateProperty(IEnumerable<TCollection> property, TModel context, string propertyName)
        {
            if (property != null)
            {
                if (property.Any())
                {
                    return new PropertyValidationResult(true, Descriptor, propertyName);
                }
            }

            return new PropertyValidationResult(false, Descriptor, propertyName);
        }
    }
}
