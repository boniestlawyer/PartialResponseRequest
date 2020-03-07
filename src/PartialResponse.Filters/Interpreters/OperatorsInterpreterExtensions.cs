using PartialResponse.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PartialResponse.Filters.Interpreters
{
    public static class OperatorsInterpreterExtensions
    {
        public static IOperatorsInterpreter<TFilter> HandleOperator<TFilter, TProp>(
            this IOperatorsInterpreter<TFilter> interpreter,
            Expression<Func<TFilter, TProp>> op, Action<string, Type> then)
        {
            string operatorType = Utils.ToPascalCase(Utils.GetMemberName(op));
            return interpreter.HandleOperator(operatorType, then);
        }

        public static IOperatorsInterpreter<TFilter> HandleOperator<TFilter>(
            this IOperatorsInterpreter<TFilter> interpreter,
            string operatorType, Action<string, Type> then)
        {
            if (interpreter.HasOperator(operatorType))
            {
                Type filterType = typeof(TFilter).GetProperties().FirstOrDefault(x => x.Name.ToLower() == operatorType.ToLower())?.PropertyType;
                var op = interpreter.GetOperator(operatorType);

                then(op.Value, op.Type);
            }

            return interpreter;
        }
    }
}
