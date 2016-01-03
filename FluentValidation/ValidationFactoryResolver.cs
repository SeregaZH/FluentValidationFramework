using FluentValidation.Validation;
using FluentValidation.Validation.Factories;
using System;

namespace FluentValidation
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
                        _validatorConfigFactory = new ValidationModelFactory();
                    }
                }
            }

            return _validatorConfigFactory;
        }
    }
}
