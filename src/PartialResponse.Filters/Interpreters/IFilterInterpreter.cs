using System;
using System.Linq.Expressions;

namespace PartialResponse.Filters.Interpreters
{
    public interface IFilterInterpreter<TFilters>
    {
        IFilterInterpreter<TFilters> HasFilter<TFilter>(Expression<Func<TFilters, TFilter>> filter, Action<OperatorsInterpreter<TFilter>> then);
    }
}