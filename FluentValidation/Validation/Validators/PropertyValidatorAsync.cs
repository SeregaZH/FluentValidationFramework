using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation.Validators
{
    public abstract class PropertyValidatorAsync<TModel, TValue> : ValidatorAsync<TModel>
    {
        private readonly Expression<Func<TModel, TValue>> _propertyGetter;
        private readonly Func<TModel, TValue> _valueResolver;

        protected PropertyValidatorAsync(ValidatorDescriptor descriptor, Expression<Func<TModel, TValue>> propertyGetter)
                 : base(descriptor)
        {
            _propertyGetter = propertyGetter;
            _valueResolver = _propertyGetter.Compile();
        }

        protected sealed override async Task<ValidationResult> ValidateModelAsync(TModel model)
        {
            return await ValidatePropertyAsync(_valueResolver(model), model);
        }

        protected abstract Task<ValidationResult> ValidatePropertyAsync(TValue value, TModel context);
    }
}
