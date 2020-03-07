using System;
using System.Linq.Expressions;

namespace PartialResponseRequest.Fields.Interpreters
{
    public interface IFieldsQueryInterpreter<TModel>
    {
        bool Includes<TProp>(Expression<Func<TModel, TProp>> field);
        IFieldsQueryInterpreter<TProp> Visit<TProp>(Expression<Func<TModel, TProp>> field);
        IFieldsQueryInterpreter<TProp> VisitList<TProp>(Expression<Func<TModel, System.Collections.Generic.IEnumerable<TProp>>> field);
    }

    public interface IFieldsQueryInterpreter
    {
        bool Includes(string fieldName);
        IFieldsQueryInterpreter Visit(string fieldName);
    }
}