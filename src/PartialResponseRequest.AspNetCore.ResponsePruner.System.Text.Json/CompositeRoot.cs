using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PartialResponseRequest.AspNetCore.ResponsePruner.OutputFormatters;
using PartialResponseRequest.AspNetCore.ResponsePruner.Pruners;
using PartialResponseRequest.AspNetCore.ResponsePruner.RequestTokenProviders;
using System;
using System.Text.Json;

namespace PartialResponseRequest.AspNetCore.ResponsePruner;

public class PartialResponseOptions {
    public Func<IServiceProvider, IJsonPruner> Pruner { get; set; } = default!;
    public Func<IServiceProvider, IRequestFieldsTokensProvider> RequestFieldsTokensProvider { get; set; } = default!;
}

public static class CompositeRoot
{
    public static IServiceCollection AddResponsePruner(
        this IServiceCollection services,
        Action<PartialResponseOptions>? optionsBuilder = null,
        JsonSerializerOptions? serialiserOptions = null)
    {
        var options = new PartialResponseOptions()
        {
            Pruner = c => new JsonPruner(),
            RequestFieldsTokensProvider = c => new RequestFieldsTokensProvider(c.GetService<IHttpContextAccessor>())
        };

        optionsBuilder?.Invoke(options);

        services.AddScoped(options.Pruner);
        services.AddScoped(options.RequestFieldsTokensProvider);
        services.AddMvcCore().AddMvcOptions(x => x.OutputFormatters.Insert(0, new JsonPartialResponseOutputFormatter(serialiserOptions)));

        return services;
    }
}
