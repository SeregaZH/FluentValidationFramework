using System;
using FluentValidation.Validation.Configuration.Enums;
using FluentValidation.Validation.Executors;

namespace FluentValidation.Validation.Factories
{
    public sealed class ValidatorExecutorFactory<TModel> : IValidatorExecutorFactory<TModel>
        where TModel : class
    {
        private readonly Func<IValidatorExecutor<TModel>> _customExecutorResolver;

        public ValidatorExecutorFactory(Func<IValidatorExecutor<TModel>> customExecutorResolver = null)
        {
            _customExecutorResolver = customExecutorResolver;
        }

        public IValidatorExecutor<TModel> Create(ValidatorExecutorTypes type)
        {
            switch (type)
            {
                case ValidatorExecutorTypes.Hierarchical:
                    return new HierarchicalValidatorExecutor<TModel>();
                case ValidatorExecutorTypes.Custom:
                    return _customExecutorResolver !=null ? _customExecutorResolver() : new PlainValidatorExecutor<TModel>();
                default:
                    return new PlainValidatorExecutor<TModel>();
            }
        }
    }
}
