using PartialResponse.Core.Enumeration;
using PartialResponse.Core.TokenReaders;
using PartialResponse.Core.TokenReaders.Utils;
using PartialResponse.Fields.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponse.Fields.TokenReaders
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
