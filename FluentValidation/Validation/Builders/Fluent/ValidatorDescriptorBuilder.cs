using FluentValidation.Validation.Models;
using System;

namespace FluentValidation.Validation.Fluent.Builders
{
    public class ValidatorDescriptorBuilder
    {
        private string _key;
        private string _descriptor;
        private string _errorMessage;

        public ValidatorDescriptorBuilder Key(string key)
        {
            _key = key;
            return this;
        }

        public ValidatorDescriptorBuilder Descriptor(string descriptor)
        {
            _descriptor = descriptor;
            return this;
        }

        public ValidatorDescriptorBuilder ErrorMessage(string message)
        {
            _errorMessage = message;
            return this;
        }

        public ValidatorDescriptor Build()
        {
            return new ValidatorDescriptor(Guid.NewGuid(), _key, _errorMessage, _descriptor);
        }
    }
}
