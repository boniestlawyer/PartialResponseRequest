using Newtonsoft.Json.Linq;
using PartialResponseRequest.Fields.Interpreters;

namespace PartialResponseRequest.AspNetCore.ResponsePruner.Pruners
{
    public interface IJsonPruner
    {
        void Prune(JToken jobject, IFieldsQueryInterpreter interpreter);
    }
}
