using FluentValidation.Helpers;
using FluentValidation.Validation.Builders.Fluent;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Options;
using FluentValidation.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DescFactory = FluentValidation.Validation.Builders.DefaultValidatorDescriptorFactory;
using LazyPropValidationDescriptor = FluentValidation.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidation.Validation.Models.PropertyName, string>>;
using LazyValValidationDescriptor = FluentValidation.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidation.Validation.Models.PropertyName, FluentValidation.Validation.Models.ValidationValue, string>>;

namespace FluentValidation.Validation.Fluent.Builders
{
    public static class ValidatorConfigBuilderExtensions
    {
        private const int HighestPriority = 0;
        private const long LowestPriority = long.MaxValue;
        private const int AsyncRulesPriority = 1;

        public static IValidationModelConfigBuilder<TModel> Required<TModel, TProperty>(
            this IValidationModelConfigBuilder<TModel> @this,
            Expression<Func<TModel, TProperty>> property,
            Func<PropertyValidatorDescriptorBuilder, LazyPropValidationDescriptor> descFactory = null,
            int? priority = null)
        {
            var baseDescriptor = DescFactory.PropertyRequired;
            var sourceDescriptor = CreatePropDescriptor(baseDescriptor, descFactory);
            var validatorContainer = new ValidatorContainer<TModel>(
                new RequiredValidator<TModel, TProperty>(sourceDescriptor, property), priority ?? LowestPriority);
            @this.AddValidator(validatorContainer);
            return @this;
        }

        public static IValidationModelConfigBuilder<TModel> CollectionRequired<TModel, TProperty>(
            this IValidationModelConfigBuilder<TModel> @this,
            Expression<Func<TModel, IEnumerable<TProperty>>> property,
            Func<PropertyValidatorDescriptorBuilder, LazyPropValidationDescriptor> descFactory = null,
            int? priority = null)
        {
            var baseDescriptor = DescFactory.CollectionPropertyRequired;
            var sourceDescriptor = CreatePropDescriptor(baseDescriptor, descFactory);
            var validatorContainer = new ValidatorContainer<TModel>(
                new CollectionRequiredValidator<TModel, TProperty>(sourceDescriptor, property), priority ?? LowestPriority);
            @this.AddValidator(validatorContainer);
            return @this;
        }

        public static IValidationModelConfigBuilder<TModel> DeniedValue<TModel, TProperty>(
            this IValidationModelConfigBuilder<TModel> @this,
            Expression<Func<TModel, TProperty>> property,
            Func<IValueValidatorOptionsBuilder<TProperty>, IValueValidatorOptions<TProperty>> optionsBuilderFactory = null,
            Func<ValueValidatorDescriptorBuilder, LazyValValidationDescriptor> descFactory = null,
            int? priority = null)
        {
            var baseDescriptor = DescFactory.DeniedValues;
            var sourceDescriptor = CreateValueDescriptor(baseDescriptor, descFactory);
            var optionsBuilder = new ValueValidatorOptionsBuilder<TProperty>();
            var validatorContainer = new ValidatorContainer<TModel>(
                new DeniedValuesValidator<TModel, TProperty>(
                    sourceDescriptor, 
                    property,
                    optionsBuilderFactory != null ? optionsBuilderFactory(optionsBuilder) : new ValueValidatorOptions<TProperty>()),
                priority ?? LowestPriority);
            @this.AddValidator(validatorContainer);
            return @this;
        }

        private static BaseLazyValidatorDescriptor<TResolver> CreatePropDescriptor<TResolver>(
            BaseLazyValidatorDescriptor<TResolver> baseDescriptor,
            Func<PropertyValidatorDescriptorBuilder, BaseLazyValidatorDescriptor<TResolver>> descFactory)
            where TResolver : class
        {
            var descriptorBuilder = new PropertyValidatorDescriptorBuilder();
            return MergePropValidatorsDescriptors(baseDescriptor, descFactory != null
                ? descFactory(descriptorBuilder) : null);
        }

        private static BaseLazyValidatorDescriptor<TResolver> CreateValueDescriptor<TResolver>(
            BaseLazyValidatorDescriptor<TResolver> baseDescriptor,
            Func<ValueValidatorDescriptorBuilder, BaseLazyValidatorDescriptor<TResolver>> descFactory)
            where TResolver : class
        {
            var descriptorBuilder = new ValueValidatorDescriptorBuilder();
            return MergePropValidatorsDescriptors(baseDescriptor, descFactory != null
                ? descFactory(descriptorBuilder) : null);
        }

        private static BaseLazyValidatorDescriptor<TResolver> MergePropValidatorsDescriptors<TResolver>(
            BaseLazyValidatorDescriptor<TResolver> @base,
            BaseLazyValidatorDescriptor<TResolver> source)
            where TResolver : class
        {
            Guard.ArgumentNull(@base, nameof(@base));
            if (source == null)
            {
                return @base;
            }

            return new BaseLazyValidatorDescriptor<TResolver>(
                source.Id,
                source.Key ?? @base.Key,
                source.ErrorMessageResolver ?? @base.ErrorMessageResolver,
                source.DescriptionResolver ?? @base.DescriptionResolver);
        }
    }
}
