using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PartialResponseRequest.Filters.Interpreters
{
    public interface IFilterQueriesInterpreter<TFilters>
    {
        bool FiltersBy<TFilter>(Expression<Func<TFilters, TFilter>> filter);

        IOperatorsInterpreter<TFilter> GetFilter<TFilter>(Expression<Func<TFilters, TFilter>> filter);
    }

    public static class IFilterQueriesInterpreterExtensions {
        
        public static bool FiltersBy<TFilters, TFilter>(this IFilterQueriesInterpreter<TFilters> interpreter,
            Expression<Func<TFilters, TFilter>> filter,
            out IOperatorsInterpreter<TFilter> operatorsInterpreter)
        {
            if(interpreter.FiltersBy(filter))
            {
                operatorsInterpreter = interpreter.GetFilter(filter);
                return true;
            }
            operatorsInterpreter = new OperatorsInterpreter<TFilter>(new List<OperatorToken>());
            return false;
        }
    }
}