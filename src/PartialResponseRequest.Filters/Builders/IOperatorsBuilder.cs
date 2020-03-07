using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponseRequest.Filters.Builders
{
    public interface IOperatorsBuilder
    {
        List<OperatorToken> Build();
    }
}
