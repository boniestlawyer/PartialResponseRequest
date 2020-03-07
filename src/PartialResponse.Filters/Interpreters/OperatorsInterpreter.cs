using PartialResponse.Core;
using PartialResponse.Filters.TokenReaders.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PartialResponse.Filters.Interpreters
{

    public class OperatorsInterpreter<TFilter> : IOperatorsInterpreter<TFilter>
    {
        private readonly Dictionary<string, OperatorToken> operators;

        public OperatorsInterpreter(List<OperatorToken> operators)
        {
            this.operators = operators.ToDictionary(x => x.Type);
        }

        public bool HasOperator<TProp>(Expression<Func<TFilter, TProp>> op)
        {
            return HasOperator(Utils.ToPascalCase(Utils.GetMemberName(op)));
        }

        public bool HasOperator(string op)
        {
            return operators.ContainsKey(op);
        }

        public OperatorValue GetOperator<TProp>(Expression<Func<TFilter, TProp>> op)
        {
            return GetOperator(Utils.ToPascalCase(Utils.GetMemberName(op)));
        }

        public OperatorValue GetOperator(string op)
        {
            Type operatorType = typeof(TFilter).GetProperties().FirstOrDefault(x => x.Name.ToLower() == op.ToLower())?.PropertyType;
            return new OperatorValue(operators[op].Value, operatorType);
        }
    }
}
