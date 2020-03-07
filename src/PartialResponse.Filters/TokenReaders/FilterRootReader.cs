using PartialResponse.Core.Enumeration;
using PartialResponse.Core.TokenReaders;
using PartialResponse.Core.TokenReaders.Utils;
using PartialResponse.Filters.TokenReaders.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace PartialResponse.Filters.TokenReaders
{
    public class FilterRootReader : ITokenReader<List<FilterToken>>
    {
        private readonly FilterReadersCollection readers;

        public FilterRootReader(FilterReadersCollection readers)
        {
            this.readers = readers;
        }

        public List<FilterToken> Read(LookupEnumerator<char> enumerator)
        {
            return readers.Get<ListOfTokensReader>().Read(enumerator, readers.Get<FilterTokenReader>(), new HashSet<char>() { '\0' }, ',').ToList();
        }
    }
}
