using PartialResponse.Core.Enumeration;
using PartialResponse.Core.TokenReaders;
using PartialResponse.Core.TokenReaders.Utils;
using PartialResponse.Fields.TokenReaders.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace PartialResponse.Fields.TokenReaders
{
    public class FieldNestedFieldsTokenReader : ITokenReader<List<FieldToken>>
    {
        private readonly HashSet<char> stopChars = new HashSet<char>(new[] { '}' });

        private readonly FieldsReadersCollection readers;

        public FieldNestedFieldsTokenReader(FieldsReadersCollection readers)
        {
            this.readers = readers;
        }

        public List<FieldToken> Read(LookupEnumerator<char> enumerator)
        {
            enumerator.MoveNext();

            var fields = readers.Get<ListOfTokensReader>().Read(enumerator, readers.Get<FieldTokenReader>(), stopChars, ',').ToList();

            enumerator.MoveNext();

            return fields;
        }
    }
}
