using PartialResponse.Filters.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponse.Filters.Builders
{
    public interface IOperatorsBuilder
    {
        List<OperatorToken> Build();
    }
}
