using System.Collections.Generic;

namespace FluentValidationFramework.IntegrationTests
{
    public class FakeModel
    {
        public string RequiredProperty { get; set; }

        public int DeniedValueProperty { get; set; }

        public IEnumerable<string> RequiredCollection { get; set; }

        public int CustomProperty { get; set; }
    }
}
