using PartialResponseRequest.Fields.TokenReaders.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace PartialResponseRequest.Fields.Serializers
{
    public class FieldsQuerySerializer : IFieldsQuerySerializer
    {
        public string Serialize(IEnumerable<FieldToken> tokens)
        {
            return string.Join(",", tokens.Select(Serialize));
        }

        public string Serialize(FieldToken token)
        {
            var name = token.Name;
            string parameters = string.Join(",", token.Parameters.Select(p => $"{p.Key}:{p.Value.Value}"));
            string nested = Serialize(token.Fields.Values);

            return $"{name}{(!string.IsNullOrEmpty(parameters) ? $"({parameters})" : "")}{(!string.IsNullOrEmpty(nested) ? $"{{{nested}}}" : "")}";
        }
    }
}
