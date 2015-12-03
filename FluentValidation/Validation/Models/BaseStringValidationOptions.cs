using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.Validation.Models
{
    public abstract class BaseStringValidationOptions
    {
        protected BaseStringValidationOptions(bool isTrimmed)
        {
            IsTrimmed = isTrimmed;
        }

        public bool IsTrimmed { get; }
    }
}
