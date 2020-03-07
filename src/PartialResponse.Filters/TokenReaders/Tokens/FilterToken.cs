using System.Collections.Generic;

namespace PartialResponse.Filters.TokenReaders.Tokens
{
    public class FilterToken
    {
        public string Field { get; private set; }
        public List<OperatorToken> Operators { get; private set; }

        public FilterToken(string field, List<OperatorToken> operators)
        {
            Field = field;
            Operators = operators;
        }
    }
}
