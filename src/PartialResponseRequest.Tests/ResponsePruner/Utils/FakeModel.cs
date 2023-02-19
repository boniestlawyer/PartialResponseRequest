using System.Collections.Generic;

namespace PartialResponseRequest.Tests.ResponsePruner.Utils;

public class FakeModel
{
    public int Include { get; set; }
    public int DontInclude { get; set; }
    public List<FakeModel> Models { get; set; } = default!;
    public FakeModel Nested { get; set; } = default!;
}
