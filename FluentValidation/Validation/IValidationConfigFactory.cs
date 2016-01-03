using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.Validation
{
    public interface IValidationConfigFactory
    {
        IValidationConfigFactory RegisterModel();
    }
}
