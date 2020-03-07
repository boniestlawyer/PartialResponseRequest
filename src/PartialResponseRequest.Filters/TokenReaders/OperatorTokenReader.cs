using PartialResponseRequest.Core.Enumeration;
using PartialResponseRequest.Core.TokenReaders;
using PartialResponseRequest.Core.TokenReaders.Utils;
using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponseRequest.Filters.TokenReaders
{
    public class OperatorTokenReader : ITokenReader<OperatorToken>
    {
        private readonly FilterReadersCollection readers;

        public OperatorTokenReader(FilterReadersCollection readers)
        {
            this.readers = readers;
        }

        public OperatorToken Read(LookupEnumerator<char> enumerator)
        {
            string key = readers.Get<SyntaxTextReader>().Read(enumerator, new HashSet<char>(new[] { ':' })).AsString();
            enumerator.MoveNext(); // current ':'
            string value = "";

            enumerator.IfNext('"', (e) =>
            {
                e.MoveNext();
                e.MoveNext();
                value = readers.Get<SyntaxTextReader>().Read(enumerator, new HashSet<char>(new[] { '"' })).AsString();
                e.MoveNext();
            }, (e) =>
            {
                e.MoveNext();
                value = readers.Get<SyntaxTextReader>().Read(enumerator, new HashSet<char>(new[] { ',', ')' })).AsString();
            });

            return new OperatorToken(key, value);
        }
    }
}
