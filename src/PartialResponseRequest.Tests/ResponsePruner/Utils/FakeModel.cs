using System.Collections.Generic;

namespace PartialResponseRequest.Tests.ResponsePruner.Utils
{
    public class FakeModel
    {
        public int Include { get; set; }
        public int DontInclude { get; set; }
        public List<FakeModel> Models { get; set; }
        public FakeModel Nested { get; set; }
    }
}
