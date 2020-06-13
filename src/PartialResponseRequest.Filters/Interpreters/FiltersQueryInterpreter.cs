using PartialResponseRequest.Core;
using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PartialResponseRequest.Filters.Interpreters
{
    public class FiltersQueryInterpreter<TFilters> : IFilterQueriesInterpreter<TFilters>
    {
        private readonly Dictionary<string, FilterToken> filters;

        public FiltersQueryInterpreter(IEnumerable<FilterToken> filters)
        {
            this.filters = filters.ToDictionary(x => x.Field);
        }

        public bool FiltersBy<TFilter>(Expression<Func<TFilters, TFilter>> filter)
        {
            string filterName = Utils.ToPascalCase(Utils.GetMemberName(filter)).ToLower();

            return filters.ContainsKey(filterName);
        }

        public IOperatorsInterpreter<TFilter> GetFilter<TFilter>(Expression<Func<TFilters, TFilter>> filter)
        {
            string filterName = Utils.ToPascalCase(Utils.GetMemberName(filter)).ToLower();

            return new OperatorsInterpreter<TFilter>(filters.ContainsKey(filterName)
                ? filters[filterName].Operators
                : new List<OperatorToken>());
        }
    }
}
