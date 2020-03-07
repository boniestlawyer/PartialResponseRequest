using PartialResponseRequest.Fields.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponseRequest.Fields.Builders
{
    public interface IFieldsQueryBuilder
    {
        List<FieldToken> Build();
    }
}
