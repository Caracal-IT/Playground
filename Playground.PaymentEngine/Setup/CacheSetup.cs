using Microsoft.Extensions.DependencyInjection;
using Playground.Core;
using Playground.PaymentEngine.Services.CacheService;

namespace Playground.PaymentEngine.Setup {
    public static class CacheSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<ICacheService, CacheService>();
            builder.Services.AddMemoryCache();
        }
    }
}