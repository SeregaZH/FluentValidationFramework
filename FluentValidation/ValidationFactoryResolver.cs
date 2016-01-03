using FluentValidation.Validation;
using FluentValidation.Validation.Factories;
using System;

namespace FluentValidation
{
    public static class ValidationFactoryResolver
    {
        private static volatile IValidationConfigFactory _validatorConfigFactory;
        private static object syncRoot = new Object();

        public static IValidationConfigFactory Resolve()
        {
            if (_validatorConfigFactory == null)
            {
                lock (syncRoot)
                {
                    if (_validatorConfigFactory == null)
                    {
                        _validatorConfigFactory = new ValidationConfigFactory();
                    }
                }
            }

            return _validatorConfigFactory;
        }
    }
}
