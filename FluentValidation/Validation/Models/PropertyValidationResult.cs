using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.Validation.Models
{
    public sealed class PropertyValidationResult : ValidationResult
    {
        public PropertyValidationResult(bool isValid, ValidatorDescriptor validatorDescriptor, string propertyName)
            : base(isValid, validatorDescriptor)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }
    }
}
