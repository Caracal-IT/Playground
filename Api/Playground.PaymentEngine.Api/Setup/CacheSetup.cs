namespace Playground.PaymentEngine.Api.Setup;

using Microsoft.Extensions.DependencyInjection;
using Core;
using Services;

public static class CacheSetup {
    public static void Setup(WebApplicationBuilder builder) {
        builder.Services.AddSingleton<ICacheService, CacheService>();
        builder.Services.AddMemoryCache();
    }
}