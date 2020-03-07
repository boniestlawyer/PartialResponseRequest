using PartialResponseRequest.Fields.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponseRequest.Fields.Serializers
{
    public interface IFieldsSerializer
    {
        string Serialize(FieldToken token);
        string Serialize(IEnumerable<FieldToken> tokens);
    }
}