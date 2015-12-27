using FluentValidation.Helpers;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DescBuilder = FluentValidation.Validation.Fluent.Builders.ValidatorDescriptorBuilder;
using DescFactory = FluentValidation.Validation.Builders.DefaultValidatorDescriptorFactory;

namespace FluentValidation.Validation.Fluent.Builders
{
    public static class ValidatorConfigBuilderExtensions
    {
        private const int HighestPriority = 0;
        private const long LowestPriority = long.MaxValue;
        private const int AsyncRulesPriority = 1;

        public static IValidationModelConfigBuilder<TModel> Required<TModel, TProperty>(
            this IValidationModelConfigBuilder<TModel> @this,
            Expression<Func<TModel,TProperty>> property,
            Func<DescBuilder, ValidatorDescriptor> descFactory = null)
        {
            var baseDescriptor = DescFactory.PropertyRequired(property.ResolvePropertyName());
            var sourceDescriptor = CreateDescriptor(baseDescriptor, descFactory);
            var validatorContainer = new ValidatorContainer<TModel>(
                new RequiredValidator<TModel, TProperty>(sourceDescriptor, property), LowestPriority);
            @this.AddValidator(validatorContainer);
            return @this;
        }

        public static IValidationModelConfigBuilder<TModel> CollectionRequired<TModel, TProperty>(
            this IValidationModelConfigBuilder<TModel> @this,
            Expression<Func<TModel, IEnumerable<TProperty>>> property,
            Func<DescBuilder, ValidatorDescriptor> descFactory = null)
        {
            var baseDescriptor = DescFactory.CollectionPropertyRequired(property.ResolvePropertyName());
            var sourceDescriptor = CreateDescriptor(baseDescriptor, descFactory);
            var validatorContainer = new ValidatorContainer<TModel>(
                new CollectionRequiredValidator<TModel, TProperty>(sourceDescriptor, property), LowestPriority);
            @this.AddValidator(validatorContainer);
            return @this;
        }

        private static ValidatorDescriptor CreateDescriptor(ValidatorDescriptor baseDescriptor, Func<DescBuilder, ValidatorDescriptor> descFactory)
        {
            var descriptorBuilder = new DescBuilder();
            return MergeValidatorsDescriptors(baseDescriptor, descFactory != null
                ? descFactory(descriptorBuilder) : null);
        }

        private static ValidatorDescriptor MergeValidatorsDescriptors(ValidatorDescriptor @base, ValidatorDescriptor source)
        {
            Guard.ArgumentNull(@base, nameof(@base));
            if (source == null)
            {
                return @base;
            }

            return new ValidatorDescriptor(
                source.Id,
                source.Key ?? @base.Key,
                source.ErrorMessage ?? @base.ErrorMessage,
                source.Description ?? @base.Description);
        }
    }
}
