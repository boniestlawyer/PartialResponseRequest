using PartialResponseRequest.Fields.Interpreters;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PartialResponseRequest.AspNetCore.ResponsePruner.Pruners;

public class WrapperAwareJsonPruner : JsonPruner
{
    private readonly string unwrap;

    public WrapperAwareJsonPruner(string wrappedDataPropertyName)
    {
        unwrap = wrappedDataPropertyName;
    }

    protected override void Prune(JsonNode node, IFieldsQueryInterpreter interpreter, int level)
    {
        if (level == 0 && IsWrapped(node))
        {
            var unwrapped = Unwrap(node);
            if (unwrapped != null)
            {
                base.Prune(unwrapped, interpreter, level++);
            }
        } else
        {
            base.Prune(node, interpreter, level);
        }
    }

    private bool IsWrapped(JsonNode? jtoken)
    {
        if (jtoken is JsonObject jobject)
        {
            if (jobject.ContainsKey(unwrap))
            {
                return true;
            }
        }
        return false;
    }

    private JsonNode? Unwrap(JsonNode? jtoken)
    {
        return IsWrapped(jtoken) ? Unwrap(jtoken?[unwrap]) : jtoken;
    }
}
