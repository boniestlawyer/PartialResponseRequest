using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PartialResponse.AspNetCore.ResponsePruner.Pruners;
using PartialResponse.AspNetCore.ResponsePruner.RequestTokenProviders;
using PartialResponse.Fields.Interpreters;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialResponse.AspNetCore.ResponsePruner.OutputFormatters
{
    public class JsonPartialResponseOutputFormatter : TextOutputFormatter
    {
        private JsonSerializer serializer;

        public JsonPartialResponseOutputFormatter()
        {
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);

            SupportedMediaTypes.Add("application/json");
            SupportedMediaTypes.Add("text/json");
            SupportedMediaTypes.Add("application/json-patch+json");
        }

        public JsonSerializer CreateJsonSerializer(JsonSerializerSettings settings) {
            if(serializer == null)
            {
                serializer = JsonSerializer.Create(settings);
            }
            return serializer;
        }

        protected virtual JsonWriter CreateJsonWriter(TextWriter writer) => new JsonTextWriter(writer)
        {
            CloseOutput = false,
            AutoCompleteOnClose = false
        };

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var pruner = context.HttpContext.RequestServices.GetService(typeof(IJsonPruner)) as IJsonPruner;
            var settings = context.HttpContext.RequestServices.GetService(typeof(IOptions<MvcJsonOptions>)) as IOptions<MvcJsonOptions>;
            var fieldsTokensProvider = context.HttpContext.RequestServices.GetService(typeof(IRequestFieldsTokensProvider)) as IRequestFieldsTokensProvider;

            var response = context.HttpContext.Response;
            using (var writer = context.WriterFactory(response.Body, selectedEncoding))
            using (var jsonWriter = CreateJsonWriter(writer))
            {
                var fieldsTokens = fieldsTokensProvider.Provide();
                var serializer = CreateJsonSerializer(settings.Value.SerializerSettings);

                if (fieldsTokens.Any())
                {
                    var tokens = JToken.FromObject(context.Object, serializer);
                    pruner.Prune(tokens, new FieldsQueryInterpreter(fieldsTokens));

                    serializer.Serialize(jsonWriter, tokens);
                }
                else
                {
                    serializer.Serialize(jsonWriter, context.Object);
                }

                await writer.FlushAsync();
            }
        }
    }
}
