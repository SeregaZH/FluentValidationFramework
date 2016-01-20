using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Validation.Models.Results;
using System.Threading;

namespace FluentValidation.Validation.Validators
{
    public abstract class AsyncValidator<TModel> : Validator<TModel>
    {
        protected sealed override ValidationResult ValidateModel(TModel model)
        {
            var validationTask = ValidateModelAsync(model);
            var suncContext = SynchronizationContext.Current;
            bool flag = false;

            Task.Factory.StartNew(() =>
            {
                suncContext.Post(delegate 
                {
                    var result = validationTask.Result;


                }, null);
            });
        }
    }
}
