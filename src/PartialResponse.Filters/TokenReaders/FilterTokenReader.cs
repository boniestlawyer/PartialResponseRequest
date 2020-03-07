using PartialResponse.Core.Enumeration;
using PartialResponse.Core.TokenReaders;
using PartialResponse.Core.TokenReaders.Utils;
using PartialResponse.Filters.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponse.Filters.TokenReaders
{
    public class FilterTokenReader : ITokenReader<FilterToken>
    {
        private readonly FilterReadersCollection readers;

        public FilterTokenReader(FilterReadersCollection readers)
        {
            this.readers = readers;
        }

        public FilterToken Read(LookupEnumerator<char> enumerator)
        {
            string field = readers.Get<SyntaxTextReader>().Read(enumerator, new HashSet<char>(new[] { '(', '{', ',', '}' })).AsString();

            List<OperatorToken> operators = new List<OperatorToken>();
            enumerator.IfNext('(', (e) =>
            {
                e.MoveNext();
                operators = readers.Get<FilterOperatorsTokenReader>().Read(e);
            });

            return new FilterToken(field, operators);
        }
    }
}
