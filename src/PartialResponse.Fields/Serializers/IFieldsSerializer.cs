using PartialResponse.Fields.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponse.Fields.Serializers
{
    public interface IFieldsSerializer
    {
        string Serialize(FieldToken token);
        string Serialize(IEnumerable<FieldToken> tokens);
    }
}