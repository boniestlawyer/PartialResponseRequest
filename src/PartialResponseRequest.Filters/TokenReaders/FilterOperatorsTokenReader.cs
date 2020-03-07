using PartialResponseRequest.Core.Enumeration;
using PartialResponseRequest.Core.TokenReaders;
using PartialResponseRequest.Core.TokenReaders.Utils;
using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace PartialResponseRequest.Filters.TokenReaders
{
    public class FilterOperatorsTokenReader : ITokenReader<List<OperatorToken>>
    {
        private readonly FilterReadersCollection readers;

        public FilterOperatorsTokenReader(FilterReadersCollection readers)
        {
            this.readers = readers;
        }
        public List<OperatorToken> Read(LookupEnumerator<char> enumerator)
        {
            enumerator.MoveNext();

            List<OperatorToken> result = readers.Get<ListOfTokensReader>().Read(
                enumerator, readers.Get<OperatorTokenReader>(), new HashSet<char>() { ')' }, ',').ToList();

            enumerator.MoveNext();

            return result;
        }
    }
}
