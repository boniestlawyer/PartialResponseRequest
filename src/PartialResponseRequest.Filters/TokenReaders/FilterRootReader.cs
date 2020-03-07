using PartialResponseRequest.Core.Enumeration;
using PartialResponseRequest.Core.TokenReaders;
using PartialResponseRequest.Core.TokenReaders.Utils;
using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace PartialResponseRequest.Filters.TokenReaders
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
