using System;
using System.Collections.Generic;

namespace FluentValidation.UnitTests
{
    public class FakeValidationTargetModel
    {
        public string StringValidationTargetProperty { get; set; }

        public int NumericValidationTargetProperty { get; set; }

        public DateTime DateValidationTargetProperty { get; set; }

        public object ReferenceTypeValidationTargetProperty { get; set; }

        public IEnumerable<object> CollectionValidationTargetProperty { get; set; }
    }
}
