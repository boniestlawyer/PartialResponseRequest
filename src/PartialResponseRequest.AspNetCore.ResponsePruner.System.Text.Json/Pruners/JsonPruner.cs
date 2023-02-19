using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using PartialResponseRequest.Fields.Interpreters;

namespace PartialResponseRequest.AspNetCore.ResponsePruner.Pruners;

public class JsonPruner : IJsonPruner
{
    public virtual void Prune(JsonNode jsonNode, IFieldsQueryInterpreter interpreter)
    {
        Prune(jsonNode, interpreter, 0);
    }

    protected virtual void Prune(JsonNode node, IFieldsQueryInterpreter interpreter, int level)
    {
        if(interpreter is EverythingIncluded)
        {
            // No prunning required
            return;
        }

        if (node is JsonObject obj)
        {
            Prune(obj, interpreter, level++);
        }
        else if (node is JsonArray jsonArray)
        {
            foreach (var item in jsonArray)
            {
                if (item != null)
                {
                    Prune(item, interpreter, level++);
                }
            }
        }
    }

    private void Prune(JsonObject element, IFieldsQueryInterpreter interpreter, int level)
    {
        foreach (var obj in element.ToList())
        {
            if (interpreter.Includes(obj.Key))
            {
                Prune(obj.Value!, interpreter.Visit(obj.Key), level++);
            }
            else
            {
                element.Remove(obj.Key);
            }
        }
    }
}
