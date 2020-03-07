using PartialResponseRequest.Core;
using PartialResponseRequest.Filters.TokenReaders.Tokens;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PartialResponseRequest.Filters.Builders
{
    public class FilterQueryBuilder<TFilterMetadata> : IFilterQueryBuilder
    {
        private readonly List<FilterToken> filterTokens = new List<FilterToken>();

        public FilterQueryBuilder<TFilterMetadata> Filter<TFilter>(
            Expression<Func<TFilterMetadata, TFilter>> field,
            Action<OperatorsBuilder<TFilter>> operatorsBuilder = null
            )
        {
            if (operatorsBuilder != null)
            {
                OperatorsBuilder<TFilter> builder = new OperatorsBuilder<TFilter>();
                operatorsBuilder(builder);
                filterTokens.Add(new FilterToken(Utils.ToPascalCase(Utils.GetMemberName(field)), builder.Build()));
            }
            else
            {
                filterTokens.Add(new FilterToken(Utils.ToPascalCase(Utils.GetMemberName(field)), new List<OperatorToken>()));
            }

            return this;
        }

        public List<FilterToken> Build()
        {
            return filterTokens;
        }
    }
}
