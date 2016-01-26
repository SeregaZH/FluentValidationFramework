using FluentValidationFramework.Validation;
using FluentValidationFramework.Validation.Factories;
using System;

namespace FluentValidationFramework
{
    public static class ValidationFactoryResolver
    {
        private static volatile IValidationModelFactory _validatorConfigFactory;
        private static object syncRoot = new Object();

        public static IValidationModelFactory Resolve()
        {
            if (_validatorConfigFactory == null)
            {
                lock (syncRoot)
                {
                    if (_validatorConfigFactory == null)
                    {
                        _validatorConfigFactory = ResolveInstance();
                    }
                }
            }

            return _validatorConfigFactory;
        }

        public static IValidationModelFactory ResolveInstance()
        {
            return new ValidationModelFactory();
        }
    }
}
