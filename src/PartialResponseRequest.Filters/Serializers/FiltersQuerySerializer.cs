using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace PartialResponseRequest.Filters.Serializers
{
    public class FiltersQuerySerializer : IFiltersSerializer
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
