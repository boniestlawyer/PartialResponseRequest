using System;
using System.Linq.Expressions;

namespace PartialResponseRequest.Filters.Interpreters
{
    public interface IOperatorsInterpreter
    {
        OperatorValue GetValue(string op);
        bool HasOperator(string op);
    }

    public interface IOperatorsInterpreter<TFilter> : IOperatorsInterpreter
    {
        OperatorValue GetValue<TProp>(Expression<Func<TFilter, TProp>> op);
        bool HasOperator<TProp>(Expression<Func<TFilter, TProp>> op);
    }

    public static class IOperatorsInterpreterExtensions
    {
        public static bool HasOperator<TFilter, TProp>(this IOperatorsInterpreter<TFilter> interpreter, Expression<Func<TFilter, TProp>> op, out OperatorValue operatorValue)
        {
            if(interpreter.HasOperator(op))
            {
                operatorValue = interpreter.GetValue(op);
                return true;
            }
            operatorValue = null;
            return false;
        }
    }
}