using Microsoft.Extensions.DependencyInjection;
using Playground.Core;
using Playground.PaymentEngine.Api.Services;

namespace Playground.PaymentEngine.Api.Setup {
    public static class CacheSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<ICacheService, CacheService>();
            builder.Services.AddMemoryCache();
        }
    }
}