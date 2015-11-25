using System;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Rules
{
  public sealed class RequiredValidator<TModel, TProperty> : PropertyValidator<TModel, TProperty>
    where TProperty : class
  {
    public RequiredValidator(ValidatorDescriptor descriptor, int priority, Expression<Func<TModel, TProperty>> propertyGetter) 
      : base(descriptor, priority, propertyGetter)
    {
    }

    protected override ValidationResult ValidateProperty(TProperty property, TModel context)
    {
      return property == default(TProperty)
        ? new ValidationResult(false, Descriptor)
        : new ValidationResult(true, Descriptor);
    }
  }
}
