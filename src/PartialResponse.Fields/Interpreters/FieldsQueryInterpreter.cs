using PartialResponse.Core;
using PartialResponse.Fields.TokenReaders.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PartialResponse.Fields.Interpreters
{
    public class FieldsQueryInterpreter<TModel> : IFieldsQueryInterpreter<TModel>
    {
        private readonly IFieldsQueryInterpreter interpreter;

        public FieldsQueryInterpreter(IFieldsQueryInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public bool Includes<TProp>(Expression<Func<TModel, TProp>> field)
        {
            return interpreter.Includes(Utils.ToPascalCase(Utils.GetMemberName(field)));
        }

        public IFieldsQueryInterpreter<TProp> VisitList<TProp>(Expression<Func<TModel, IEnumerable<TProp>>> field)
        {
            return new FieldsQueryInterpreter<TProp>(interpreter.Visit(Utils.ToPascalCase(Utils.GetMemberName(field))));
        }

        public IFieldsQueryInterpreter<TProp> Visit<TProp>(Expression<Func<TModel, TProp>> field)
        {
            return new FieldsQueryInterpreter<TProp>(interpreter.Visit(Utils.ToPascalCase(Utils.GetMemberName(field))));
        }
    }

    public class FieldsQueryInterpreter : IFieldsQueryInterpreter
    {
        private readonly Dictionary<string, FieldToken> tokens;

        public FieldsQueryInterpreter()
            : this(new FieldToken[0])
        {
        }

        public FieldsQueryInterpreter(IEnumerable<FieldToken> tokens)
            : this(tokens.ToDictionary(x => x.Name, x => x))
        {
        }
        public FieldsQueryInterpreter(Dictionary<string, FieldToken> tokens)
        {
            this.tokens = tokens;
        }

        public bool Includes(string fieldName) => tokens.Count == 0 || tokens.ContainsKey(fieldName) || tokens.ContainsKey("*");

        public IFieldsQueryInterpreter Visit(string fieldName)
        {
            if (tokens.ContainsKey(fieldName))
            {
                var field = tokens[fieldName];
                return new FieldsQueryInterpreter(field.Fields);
            }

            if (Includes(fieldName))
            {
                return new EverythingIncluded();
            }
            else
            {
                return new NothingIncluded();
            }
        }
    }
}
