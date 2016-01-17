using FluentValidation.Helpers;
using FluentValidation.Validation.Models;
using System;

namespace FluentValidation.Validation.Builders.Fluent
{
    public class BaseValidatorDescriptorBuilder<TBuilder, TResolver>
        where TBuilder: BaseValidatorDescriptorBuilder<TBuilder, TResolver>
        where TResolver: class
    {
        private string _key;
        private TResolver _descriptorResolver;
        private TResolver _messageResolver;

        public TBuilder WithPropertyDescriptor(TResolver descriptorResolver)
        {
            _descriptorResolver = descriptorResolver;
            return this as TBuilder;
        }

        /// <summary>
        /// Assign the custom errors message.
        /// </summary>
        /// <param name="message">The custorm message.</param>
        /// <returns>The builder.</returns>
        public TBuilder WithPropertyErrorMessage(TResolver messageResolver)
        {
            _messageResolver = messageResolver;
            return this as TBuilder;
        }

        /// <summary>
        /// Assign the custorm validator key.
        /// </summary>
        /// <param name="key">The custom validator key.</param>
        /// <returns>The builder.</returns>
        public TBuilder WithKey(string key)
        {
            Guard.ArgumentNullEmptyOrWhiteSpace(key, nameof(key));
            _key = key;
            return this as TBuilder;
        }

        public BaseLazyValidatorDescriptor<TResolver> Build()
        {
            return new BaseLazyValidatorDescriptor<TResolver>(Guid.NewGuid(), _key, _messageResolver, _descriptorResolver);
        }
    }
}
