using PartialResponseRequest.Core.Enumeration;
using PartialResponseRequest.Core.TokenReaders;
using PartialResponseRequest.Core.TokenReaders.Utils;
using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponseRequest.Filters.TokenReaders
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
