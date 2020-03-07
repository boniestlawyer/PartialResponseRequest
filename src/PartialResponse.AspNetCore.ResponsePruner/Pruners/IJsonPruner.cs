using Newtonsoft.Json.Linq;
using PartialResponse.Fields.Interpreters;

namespace PartialResponse.AspNetCore.ResponsePruner.Pruners
{
    public interface IJsonPruner
    {
        void Prune(JToken jobject, IFieldsQueryInterpreter interpreter);
    }
}
