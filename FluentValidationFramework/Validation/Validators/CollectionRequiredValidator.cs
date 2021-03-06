﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidationFramework.Validation.Models;
using LazyPropValidationDescriptor = FluentValidationFramework.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidationFramework.Validation.Models.PropertyName, string>>;

namespace FluentValidationFramework.Validation.Validators
{
    public sealed class CollectionRequiredValidator<TModel, TCollection> : SyncPropertyValidator<TModel, IEnumerable<TCollection>>
    {
        private readonly LazyPropValidationDescriptor _lazyPropertyDescriptor;

        public CollectionRequiredValidator(
            LazyPropValidationDescriptor lazyPropertyDescriptor,
            Expression<Func<TModel, IEnumerable<TCollection>>> propertyGetter) 
            : base(propertyGetter)
        {
            _lazyPropertyDescriptor = lazyPropertyDescriptor;
        }

        protected override PropertyValidationResult ValidateProperty(IEnumerable<TCollection> property, TModel context, string propertyName)
        {
            if (property != null)
            {
                if (property.Any())
                {
                    return new PropertyValidationResult(true, DescriptorResolver.Resolve(propertyName, _lazyPropertyDescriptor), propertyName);
                }
            }

            return new PropertyValidationResult(false, DescriptorResolver.Resolve(propertyName, _lazyPropertyDescriptor), propertyName);
        }
    }
}
