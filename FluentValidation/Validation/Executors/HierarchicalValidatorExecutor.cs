using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Results;
using FluentValidation.Helpers;
using System.Linq;

namespace FluentValidation.Validation.Executors
{
    public sealed class HierarchicalValidatorExecutor<TModel> : IValidatorExecutor<TModel>
        where TModel: class
    {
        public IEnumerable<ValidationResult> Execute(TModel model, IEnumerable<ValidatorContainer<TModel>> validatorContainers)
        {
            Guard.ArgumentNull(model, nameof(model));
            Guard.ArgumentNull(validatorContainers, nameof(validatorContainers));
            var groupedValidators = GroupValidators(validatorContainers);
            var allResults = new List<ValidationResult>();
            foreach (var group in groupedValidators)
            {
                var results = ExecuteGroup(group, model);
                allResults.AddRange(results);
                if (results.Any(x => !x.IsValid()))
                {
                    return allResults;
                }
            }

            return allResults;
        }

        public async Task<IEnumerable<ValidationResult>> ExecuteAsync(TModel model, IEnumerable<ValidatorContainer<TModel>> validatorContainers)
        {
            Guard.ArgumentNull(model, nameof(model));
            Guard.ArgumentNull(validatorContainers, nameof(validatorContainers));
            var groupedValidators = GroupValidators(validatorContainers);
            var allResults = new List<ValidationResult>();
            foreach (var group in groupedValidators)
            {
                var results = await ExecuteGroupAsync(group, model);
                allResults.AddRange(results);
                if (results.Any(x => !x.IsValid()))
                {
                    return allResults;
                }
            }

            return allResults;
        }

        private IEnumerable<ValidationResult> ExecuteGroup(IGrouping<long, IValidator<TModel>> group, TModel model)
        {
            return group.Select(x => x.Validate(model));
        }

        private async Task<IEnumerable<ValidationResult>> ExecuteGroupAsync(IGrouping<long, IValidator<TModel>> group, TModel model)
        {
            return await Task.WhenAll(group.Select(async x => await x.ValidateAsync(model)));
        }

        private IEnumerable<IGrouping<long, IValidator<TModel>>> GroupValidators(IEnumerable<ValidatorContainer<TModel>> validatorContainers)
        {
            return validatorContainers
                .GroupBy(x => x.Priority, x => x.Validator)
                .OrderBy(x => x.Key);
        }
    }
}
