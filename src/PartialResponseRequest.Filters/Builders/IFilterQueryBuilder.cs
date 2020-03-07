using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System.Collections.Generic;

namespace PartialResponseRequest.Filters.Builders
{
    public interface IFilterQueryBuilder
    {
        List<FilterToken> Build();
    }
}
