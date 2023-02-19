using Microsoft.AspNetCore.Mvc.Formatters;
using PartialResponseRequest.AspNetCore.ResponsePruner.Pruners;
using PartialResponseRequest.AspNetCore.ResponsePruner.RequestTokenProviders;
using PartialResponseRequest.Fields.Interpreters;
using System.Text;
using System.Text.Json;

namespace PartialResponseRequest.AspNetCore.ResponsePruner.OutputFormatters;

public class JsonPartialResponseOutputFormatter : TextOutputFormatter
{
    private readonly JsonSerializerOptions? _serialiserOptions;

    public JsonPartialResponseOutputFormatter(JsonSerializerOptions? serialiserOptions = null)
    {
        _serialiserOptions = serialiserOptions;
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);

        SupportedMediaTypes.Add("application/json");
        SupportedMediaTypes.Add("text/json");
        SupportedMediaTypes.Add("application/json-patch+json");
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var pruner = context.HttpContext.RequestServices.GetService(typeof(IJsonPruner)) as IJsonPruner;
        var fieldsTokensProvider = context.HttpContext.RequestServices.GetService(typeof(IRequestFieldsTokensProvider)) as IRequestFieldsTokensProvider;

        var response = context.HttpContext.Response;
        var jsonWriter = new Utf8JsonWriter(response.Body);

        var fieldsTokens = fieldsTokensProvider?.Provide();

        var node =
            System.Text.Json.JsonSerializer.SerializeToNode(context.Object, _serialiserOptions)!;

        if (fieldsTokens?.Any() == true)
        {
            pruner?.Prune(node, new FieldsQueryInterpreter(fieldsTokens));
        }

        node.WriteTo(jsonWriter);

        await jsonWriter.FlushAsync();
    }
}
