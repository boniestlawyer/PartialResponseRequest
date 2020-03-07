using PartialResponse.Core.Enumeration;
using PartialResponse.Core.TokenReaders;
using PartialResponse.Core.TokenReaders.Utils;
using PartialResponse.Filters.TokenReaders.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace PartialResponse.Filters.TokenReaders
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
