using PartialResponseRequest.Core.Enumeration;
using PartialResponseRequest.Core.TokenReaders;
using PartialResponseRequest.Core.TokenReaders.Utils;
using PartialResponseRequest.Fields.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponseRequest.Fields.TokenReaders
{
    public class FieldTokenReader : ITokenReader<FieldToken>
    {
        private readonly FieldsReadersCollection readers;

        public FieldTokenReader(FieldsReadersCollection readers)
        {
            this.readers = readers;
        }

        public FieldToken Read(LookupEnumerator<char> enumerator)
        {
            string name = readers.Get<SyntaxTextReader>().Read(enumerator, new HashSet<char>(new[] { '(', '{', ',', '}' })).AsString();

            List<ParameterToken> properties = new List<ParameterToken>();
            enumerator.IfNext('(', (e) =>
            {
                e.MoveNext();
                properties = readers.Get<FieldPropertiesTokenReader>().Read(e);
            });

            List<FieldToken> nested = new List<FieldToken>();
            enumerator.IfNext('{', (e) =>
            {
                e.MoveNext();
                nested = readers.Get<FieldNestedFieldsTokenReader>().Read(e);
            });

            return new FieldToken(name, properties, nested);
        }
    }
}
