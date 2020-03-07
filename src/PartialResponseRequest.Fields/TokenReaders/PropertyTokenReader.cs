using PartialResponseRequest.Core.Enumeration;
using PartialResponseRequest.Core.TokenReaders;
using PartialResponseRequest.Core.TokenReaders.Utils;
using PartialResponseRequest.Fields.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponseRequest.Fields.TokenReaders
{
    public class PropertyTokenReader : ITokenReader<ParameterToken>
    {
        private readonly FieldsReadersCollection readers;

        public PropertyTokenReader(FieldsReadersCollection readers)
        {
            this.readers = readers;
        }

        public ParameterToken Read(LookupEnumerator<char> enumerator)
        {
            string key = readers.Get<SyntaxTextReader>().Read(enumerator, new HashSet<char>(new[] { ':' })).AsString();
            enumerator.MoveNext(); // SKIP ':'
            enumerator.MoveNext(); // Move to first char after ':'
            string value = readers.Get<SyntaxTextReader>().Read(enumerator, new HashSet<char>(new[] { ',', ')' })).AsString();

            return new ParameterToken(key, value);
        }
    }
}
