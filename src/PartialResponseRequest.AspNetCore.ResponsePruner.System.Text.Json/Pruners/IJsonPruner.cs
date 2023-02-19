using PartialResponseRequest.Fields.Interpreters;
using System.Text.Json.Nodes;

namespace PartialResponseRequest.AspNetCore.ResponsePruner.Pruners;

public interface IJsonPruner
{
    void Prune(JsonNode jsonNode, IFieldsQueryInterpreter interpreter);
}
