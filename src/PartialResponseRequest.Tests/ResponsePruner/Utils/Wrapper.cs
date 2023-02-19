using System;
using System.Collections.Generic;
using System.Text;

namespace PartialResponseRequest.Tests.ResponsePruner.Utils;

public class Wrapper<T>
{
    public T Data { get; set; } = default!;
}
