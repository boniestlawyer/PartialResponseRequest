using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponseRequest.Filters.Serializers
{
    public interface IFiltersSerializer
    {
        string Serialize(FilterToken token);
        string Serialize(IEnumerable<FilterToken> filters);
    }
}