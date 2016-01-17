using FluentValidation.Helpers;
using FluentValidation.Validation.Builders.Fluent;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DescFactory = FluentValidation.Validation.Builders.DefaultValidatorDescriptorFactory;
using LazyPropValidationDescriptor = FluentValidation.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidation.Validation.Models.PropertyName, string>>;

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
            Func<PropertyValidatorDescriptorBuilder, LazyPropValidationDescriptor> descFactory = null)
        {
            var baseDescriptor = DescFactory.PropertyRequired;
            var sourceDescriptor = CreatePropDescriptor(baseDescriptor, descFactory);
            var validatorContainer = new ValidatorContainer<TModel>(
                new RequiredValidator<TModel, TProperty>(sourceDescriptor, property), LowestPriority);
            @this.AddValidator(validatorContainer);
            return @this;
        }

        public static IValidationModelConfigBuilder<TModel> CollectionRequired<TModel, TProperty>(
            this IValidationModelConfigBuilder<TModel> @this,
            Expression<Func<TModel, IEnumerable<TProperty>>> property,
            Func<PropertyValidatorDescriptorBuilder, LazyPropValidationDescriptor> descFactory = null)
        {
            var baseDescriptor = DescFactory.CollectionPropertyRequired;
            var sourceDescriptor = CreatePropDescriptor(baseDescriptor, descFactory);
            var validatorContainer = new ValidatorContainer<TModel>(
                new CollectionRequiredValidator<TModel, TProperty>(sourceDescriptor, property), LowestPriority);
            @this.AddValidator(validatorContainer);
            return @this;
        }

        private static LazyPropValidationDescriptor CreatePropDescriptor(
            LazyPropValidationDescriptor baseDescriptor, 
            Func<PropertyValidatorDescriptorBuilder, LazyPropValidationDescriptor> descFactory)
        {
            var descriptorBuilder = new PropertyValidatorDescriptorBuilder();
            return MergePropValidatorsDescriptors(baseDescriptor, descFactory != null
                ? descFactory(descriptorBuilder) : null);
        }

        private static LazyPropValidationDescriptor MergePropValidatorsDescriptors(
            LazyPropValidationDescriptor @base,
            LazyPropValidationDescriptor source)
        {
            Guard.ArgumentNull(@base, nameof(@base));
            if (source == null)
            {
                return @base;
            }

            return new LazyPropValidationDescriptor(
                source.Id,
                source.Key ?? @base.Key,
                source.ErrorMessageResolver ?? @base.ErrorMessageResolver,
                source.DescriptionResolver ?? @base.DescriptionResolver);
        }
    }
}
