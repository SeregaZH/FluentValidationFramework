using System;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;


namespace FluentValidation.Validation.Rules
{
  public abstract class PropertyValidator<TModel, TProperty> : Validator<TModel>
  {
    private readonly Expression<Func<TModel, TProperty>> _propertyGetter;

    protected PropertyValidator(ValidatorDescriptor descriptor, int priority, Expression<Func<TModel, TProperty>> propertyGetter)
      : base(descriptor, priority)
    {
      _propertyGetter = propertyGetter;
    }

    protected sealed override ValidationResult ValidateModel(TModel model)
    {
      var method = _propertyGetter.Compile();
      return ValidateProperty(method(model), model);
    }

    protected abstract ValidationResult ValidateProperty(TProperty property, TModel context);
  }
}
