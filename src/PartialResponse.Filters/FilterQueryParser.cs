using PartialResponse.Core.Enumeration;
using PartialResponse.Filters.TokenReaders;
using PartialResponse.Filters.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponse.Filters
{
    public class FilterQueryParser
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
