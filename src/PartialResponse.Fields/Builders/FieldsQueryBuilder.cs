using PartialResponse.Core;
using PartialResponse.Fields.TokenReaders.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PartialResponse.Fields.Builders
{
    public class FieldsQueryBuilder<TModel> : IFieldsQueryBuilder
    {
        private readonly List<FieldToken> fields = new List<FieldToken>();

        public FieldsQueryBuilder<TModel> Everything()
        {
            fields.Add(new FieldToken("*", new List<ParameterToken>(), new List<FieldToken>()));

            return this;
        }

        public FieldsQueryBuilder<TModel> Field<TProp>(Expression<Func<TModel, TProp>> field, IDictionary<string, string> parameters = null)
        {
            var fieldName = Utils.ToPascalCase(Utils.GetMemberName(field));
            var fieldParams = (parameters ?? new Dictionary<string, string>()).Select(x => new ParameterToken(x.Key, x.Value)).ToList();

            fields.Add(new FieldToken(
                fieldName,
                fieldParams,
                new List<FieldToken>()));

            return this;
        }

        public FieldsQueryBuilder<TModel> NestedList<TProp>(
            Expression<Func<TModel, IEnumerable<TProp>>> field,
            Action<FieldsQueryBuilder<TProp>> nestedBuilder
            )
        {
            FieldsQueryBuilder<TProp> builder = new FieldsQueryBuilder<TProp>();
            nestedBuilder(builder);

            var fieldName = Utils.ToPascalCase(Utils.GetMemberName(field));
            fields.Add(new FieldToken(fieldName, new List<ParameterToken>(), builder.Build()));

            return this;
        }

        public FieldsQueryBuilder<TModel> Nested<TProp>(
            Expression<Func<TModel, TProp>> field,
            Action<FieldsQueryBuilder<TProp>> nestedBuilder
            )
        {
            FieldsQueryBuilder<TProp> builder = new FieldsQueryBuilder<TProp>();
            nestedBuilder(builder);

            var fieldName = Utils.ToPascalCase(Utils.GetMemberName(field));
            fields.Add(new FieldToken(fieldName, new List<ParameterToken>(), builder.Build()));

            return this;
        }

        public List<FieldToken> Build()
        {
            return fields;
        }
    }
}
