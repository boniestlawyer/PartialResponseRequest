using PartialResponseRequest.Core.Enumeration;
using PartialResponseRequest.Filters.TokenReaders;
using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponseRequest.Filters
{
    public class FiltersQueryParser
    {
        public IEnumerable<FilterToken> Parse(string code)
        {
            FilterReadersCollection readers = new FilterReadersCollection();
            FilterRootReader root = readers.Get<FilterRootReader>();

            LookupEnumerator<char> enumerator = new LookupEnumerator<char>(code.GetEnumerator());
            enumerator.MoveNext();

            return root.Read(enumerator);
        }
    }
}
