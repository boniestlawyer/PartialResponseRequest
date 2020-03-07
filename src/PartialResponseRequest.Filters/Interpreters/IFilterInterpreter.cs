using System;
using System.Linq.Expressions;

namespace PartialResponseRequest.Filters.Interpreters
{
    public interface IFilterInterpreter<TFilters>
    {
        IFilterInterpreter<TFilters> HasFilter<TFilter>(Expression<Func<TFilters, TFilter>> filter, Action<OperatorsInterpreter<TFilter>> then);
    }
}