using PartialResponse.Fields.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponse.Fields.Builders
{
    public interface IFieldsQueryBuilder
    {
        List<FieldToken> Build();
    }
}
