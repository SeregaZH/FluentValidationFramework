using FluentValidation.Helpers;
using FluentValidation.Validation.Models;
using System;

namespace FluentValidation.Validation.Fluent.Builders
{
    /// <summary>
    /// Represent builder to create validator descriptor.
    /// </summary>
    public class ValidatorDescriptorBuilder
    {
        private string _key;
        private string _descriptor;
        private string _errorMessage;

        /// <summary>
        /// Assign the custorm validator key.
        /// </summary>
        /// <param name="key">The custom validator key.</param>
        /// <returns>The builder.</returns>
        public ValidatorDescriptorBuilder Key(string key)
        {
            Guard.ArgumentNullEmptyOrWhiteSpace(key, nameof(key));
            _key = key;
            return this;
        }

        /// <summary>
        /// Assign the custom validator descriptor.
        /// </summary>
        /// <param name="descriptor">The custom descriptor.</param>
        /// <returns>The builder.</returns>
        public ValidatorDescriptorBuilder Descriptor(string descriptor)
        {
            _descriptor = descriptor;
            return this;
        }

        /// <summary>
        /// Assign the custom errors message.
        /// </summary>
        /// <param name="message">The custorm message.</param>
        /// <returns>The builder.</returns>
        public ValidatorDescriptorBuilder ErrorMessage(string message)
        {
            Guard.ArgumentNull(message, nameof(message));
            _errorMessage = message;
            return this;
        }

        /// <summary>
        /// Build the descriptor.
        /// </summary>
        /// <returns>The validation descriptor.</returns>
        public ValidatorDescriptor Build()
        {
            return new ValidatorDescriptor(Guid.NewGuid(), _key, _errorMessage, _descriptor);
        }
    }
}
