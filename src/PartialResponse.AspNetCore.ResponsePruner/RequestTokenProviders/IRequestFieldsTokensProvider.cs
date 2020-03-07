using System.Collections.Generic;
using PartialResponse.Fields.TokenReaders.Tokens;

namespace PartialResponse.AspNetCore.ResponsePruner.RequestTokenProviders
{
    public interface IRequestFieldsTokensProvider
    {
        List<FieldToken> Provide();
    }
}