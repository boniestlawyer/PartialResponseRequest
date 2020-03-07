using PartialResponseRequest.Core;
using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PartialResponseRequest.Filters.Builders
{
    public class OperatorsBuilder<TFilter> : IOperatorsBuilder
    {
        private readonly List<OperatorToken> operatorTokens = new List<OperatorToken>();

        public OperatorsBuilder<TFilter> Operator<TProp>(Expression<Func<TFilter, TProp>> op, string value)
        {
            operatorTokens.Add(new OperatorToken(Utils.ToPascalCase(Utils.GetMemberName(op)), value));
            return this;
        }

        public List<OperatorToken> Build()
        {
            return operatorTokens;
        }
    }
}
