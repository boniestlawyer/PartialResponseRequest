using PartialResponse.Filters.TokenReaders.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace PartialResponse.Filters.Serializers
{
    public class FiltersSerializer : IFiltersSerializer
    {
        public string Serialize(IEnumerable<FilterToken> filters)
        {
            return string.Join(",", filters.Select(Serialize));
        }

        public string Serialize(FilterToken token)
        {
            string filterName = token.Field;
            string operators = string.Join(",", token.Operators.Select(x => $"{x.Type}:{x.Value}"));

            return $"{token.Field}{(!string.IsNullOrEmpty(operators) ? $"({operators})" : "")}";
        }
    }
}
