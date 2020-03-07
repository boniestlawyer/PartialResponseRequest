using PartialResponse.Filters.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponse.Filters.Serializers
{
    public interface IFiltersSerializer
    {
        string Serialize(FilterToken token);
        string Serialize(IEnumerable<FilterToken> filters);
    }
}