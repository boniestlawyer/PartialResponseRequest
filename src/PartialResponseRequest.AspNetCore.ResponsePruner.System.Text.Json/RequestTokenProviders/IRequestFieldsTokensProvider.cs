using PartialResponseRequest.Fields.TokenReaders.Tokens;

namespace PartialResponseRequest.AspNetCore.ResponsePruner.RequestTokenProviders;

public interface IRequestFieldsTokensProvider
{
    List<FieldToken>? Provide();
}
