﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Options;
using LazyValValidationDescriptor = FluentValidation.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidation.Validation.Models.PropertyName, FluentValidation.Validation.Models.ValidationValue, string>>;

namespace FluentValidation.Validation.Validators
{
    public sealed class DeniedStringValuesValidator<TModel> : DeniedValuesValidator<TModel, string>
    {
        private readonly StringValuesValidatorOptions _stringOptions;

        public DeniedStringValuesValidator(
            LazyValValidationDescriptor lazyValueDescriptor,
            Expression<Func<TModel, string>> propertyGetter,
            StringValuesValidatorOptions options) 
            : base(lazyValueDescriptor, propertyGetter, new ValueValidatorOptions<string>(new HashSet<string>(options.Values.Select(x => options.IsTrimmed ? x?.Trim() : x)), options.Comparer))
        {
            _stringOptions = options;
        }

        protected override PropertyValidationResult ValidateValue(string value, TModel context, string propertyName)
        {
            return base.ValidateValue(_stringOptions.IsTrimmed ? value?.Trim() : value, context, propertyName);
        }
    }
}
