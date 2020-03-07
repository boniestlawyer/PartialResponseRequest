using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PartialResponse.AspNetCore.ResponsePruner.OutputFormatters;
using PartialResponse.AspNetCore.ResponsePruner.Pruners;
using PartialResponse.AspNetCore.ResponsePruner.RequestTokenProviders;
using System;

namespace PartialResponse.AspNetCore.ResponsePruner
{
    public class PartialResponseOptions {
        public Func<IServiceProvider, IJsonPruner> Pruner { get; set; }
        public Func<IServiceProvider, IRequestFieldsTokensProvider> RequestFieldsTokensProvider { get; set; }
    }

    public static class CompositeRoot
    {
        public static IServiceCollection AddPartialResponse(this IServiceCollection services, Action<PartialResponseOptions> optionsBuilder = null)
        {
            var options = new PartialResponseOptions()
            {
                Pruner = c => new JsonPruner(),
                RequestFieldsTokensProvider = c => new RequestFieldsTokensProvider(c.GetService<IHttpContextAccessor>())
            };

            optionsBuilder?.Invoke(options);

            services.AddScoped(options.Pruner);
            services.AddScoped(options.RequestFieldsTokensProvider);
            services.AddMvcCore().AddMvcOptions(x => x.OutputFormatters.Insert(0, new JsonPartialResponseOutputFormatter()));

            return services;
        }
    }
}
