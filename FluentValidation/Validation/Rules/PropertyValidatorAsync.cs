using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Rules
{
  public abstract class PropertyValidatorAsync<TModel, TProperty>  : ValidatorAsync<TModel>
  {
    private readonly Expression<Func<TModel, TProperty>> _propertyGetter;

    protected PropertyValidatorAsync(ValidatorDescriptor descriptor, int priority, Expression<Func<TModel, TProperty>> propertyGetter)
      : base(descriptor, priority)
    {
      _propertyGetter = propertyGetter;
    }

    protected sealed override async Task<ValidationResult> ValidateModelAsync(TModel model)
    {
      var method = _propertyGetter.Compile();
      return await ValidatePropertyAsync(method(model), model);
    }

    protected abstract Task<ValidationResult> ValidatePropertyAsync(TProperty property, TModel context);
  }
}
