using System.Collections.Generic;
using System.Linq;

namespace PartialResponseRequest.Fields.TokenReaders.Tokens
{
    public class FieldToken
    {
        public string Name { get; private set; }
        public Dictionary<string, ParameterToken> Parameters { get; private set; }
        public Dictionary<string, FieldToken> Fields { get; private set; }

        public FieldToken(string name, List<ParameterToken> properties, List<FieldToken> nested)
        {
            Name = name;
            Parameters = properties.ToDictionary(x => x.Name, x => x);
            Fields = nested.ToDictionary(x => x.Name, x => x);
        }
    }
}
