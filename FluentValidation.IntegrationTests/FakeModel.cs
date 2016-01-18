using System.Collections.Generic;

namespace FluentValidation.IntegrationTests
{
    internal class FakeModel
    {
        internal string RequiredProperty { get; set; }

        internal int DeniedValueProperty { get; set; }

        internal IEnumerable<string> RequiredCollection { get; set; }
    }
}
