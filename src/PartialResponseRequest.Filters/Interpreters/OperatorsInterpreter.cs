using PartialResponseRequest.Core;
using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PartialResponseRequest.Filters.Interpreters
{
    public class OperatorsInterpreter<TFilter> : IOperatorsInterpreter<TFilter>
    {
        private readonly Dictionary<string, OperatorToken> operators;

        public OperatorsInterpreter(List<OperatorToken> operators)
        {
            this.operators = operators.ToDictionary(x => x.Type.ToLower());
        }

        public bool HasOperator<TProp>(Expression<Func<TFilter, TProp>> op)
        {
            return HasOperator(Utils.ToPascalCase(Utils.GetMemberName(op)));
        }

        public bool HasOperator(string op)
        {
            return operators.ContainsKey(op.ToLower());
        }

        public OperatorValue GetValue<TProp>(Expression<Func<TFilter, TProp>> op)
        {
            return GetValue(Utils.ToPascalCase(Utils.GetMemberName(op)));
        }

        public OperatorValue GetValue(string op)
        {
            Type operatorType = typeof(TFilter).GetProperties().FirstOrDefault(x => x.Name.ToLower() == op.ToLower())?.PropertyType;
            return new OperatorValue(operators[op.ToLower()].Value, operatorType);
        }
    }
}
