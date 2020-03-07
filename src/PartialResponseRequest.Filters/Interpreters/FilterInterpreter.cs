using PartialResponseRequest.Core;
using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PartialResponseRequest.Filters.Interpreters
{
    public class FilterInterpreter<TFilters> : IFilterInterpreter<TFilters>
    {
        private readonly Dictionary<string, FilterToken> filters;

        public FilterInterpreter(List<FilterToken> filters)
        {
            this.filters = filters.ToDictionary(x => x.Field);
        }

        public IFilterInterpreter<TFilters> HasFilter<TFilter>(Expression<Func<TFilters, TFilter>> filter,
            Action<OperatorsInterpreter<TFilter>> then)
        {
            string filterName = Utils.ToPascalCase(Utils.GetMemberName(filter));
            if (filters.ContainsKey(filterName))
            {
                OperatorsInterpreter<TFilter> operatorInterpreter = new OperatorsInterpreter<TFilter>(filters[filterName].Operators);
                then(operatorInterpreter);
            }

            return this;
        }
    }
}
