using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PartialResponse.Fields.Interpreters;

namespace PartialResponse.AspNetCore.ResponsePruner.Pruners
{
    public class JsonPruner : IJsonPruner
    {
        public virtual void Prune(JToken token, IFieldsQueryInterpreter interpreter)
        {
            Prune(token, interpreter, 0);
        }

        protected virtual void Prune(JToken token, IFieldsQueryInterpreter interpreter, int level)
        {
            if(interpreter is EverythingIncluded)
            {
                // No prunning required
                return;
            }

            if (token is JObject jobject)
            {
                Prune(jobject, interpreter, level);
            }
            if (token is JArray jarray)
            {
                foreach (var obj in jarray.OfType<JObject>())
                {
                    Prune(obj, interpreter, level);
                }
            }
        }

        private void Prune(JObject jobject, IFieldsQueryInterpreter interpreter, int level)
        {
            foreach (var key in jobject.Properties().ToList())
            {
                if (interpreter.Includes(key.Name))
                {
                    Prune(key.Value, interpreter.Visit(key.Name), level++);
                }
                else
                {
                    key.Remove();
                }
            }
        }
    }
}
