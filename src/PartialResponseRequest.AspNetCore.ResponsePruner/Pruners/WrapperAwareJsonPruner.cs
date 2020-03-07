using Newtonsoft.Json.Linq;
using PartialResponseRequest.Fields.Interpreters;

namespace PartialResponseRequest.AspNetCore.ResponsePruner.Pruners
{
    public class WrapperAwareJsonPruner : JsonPruner
    {
        private readonly string unwrap;

        public WrapperAwareJsonPruner(string wrappedDataPropertyName)
        {
            unwrap = wrappedDataPropertyName;
        }

        protected override void Prune(JToken token, IFieldsQueryInterpreter interpreter, int level)
        {
            if (level == 0 && IsWrapped(token))
            {
                base.Prune(Unwrap(token), interpreter, level++);
            } else
            {
                base.Prune(token, interpreter, level);
            }
        }

        private bool IsWrapped(JToken jtoken)
        {
            if (jtoken is JObject jobject)
            {
                if (jobject.ContainsKey(unwrap))
                {
                    return true;
                }
            }
            return false;
        }

        private JToken Unwrap(JToken jtoken)
        {
            return IsWrapped(jtoken) ? Unwrap(jtoken[unwrap]) : jtoken;
        }
    }
}
