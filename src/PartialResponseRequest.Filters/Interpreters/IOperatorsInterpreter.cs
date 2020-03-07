using System;
using System.Linq.Expressions;

namespace PartialResponseRequest.Filters.Interpreters
{
    public interface IOperatorsInterpreter
    {
        OperatorValue GetOperator(string op);
        bool HasOperator(string op);
    }

    public interface IOperatorsInterpreter<TFilter> : IOperatorsInterpreter
    {
        OperatorValue GetOperator<TProp>(Expression<Func<TFilter, TProp>> op);
        bool HasOperator<TProp>(Expression<Func<TFilter, TProp>> op);
    }
}