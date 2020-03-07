using Microsoft.AspNetCore.Http;
using PartialResponse.Fields;
using PartialResponse.Fields.TokenReaders.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace PartialResponse.AspNetCore.ResponsePruner.RequestTokenProviders
{
    public class RequestFieldsTokensProvider : IRequestFieldsTokensProvider
    {
        private readonly IHttpContextAccessor accessor;
        private readonly FieldsQueryParser parser = new FieldsQueryParser();

        public RequestFieldsTokensProvider(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public List<FieldToken> Provide()
        {
            var query = accessor.HttpContext.Request.Query["fields"];
            if (string.IsNullOrEmpty(query))
            {
                return new List<FieldToken>();
            }

            if (!accessor.HttpContext.Items.ContainsKey("field-tokens"))
            {
                accessor.HttpContext.Items["field-tokens"] = parser.Parse(query).ToList();
            }

            return accessor.HttpContext.Items["field-tokens"] as List<FieldToken>;
        }
    }
}
