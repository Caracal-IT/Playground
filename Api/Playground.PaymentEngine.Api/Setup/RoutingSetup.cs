using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.Api.Services;
using Playground.PaymentEngine.Api.Terminals.Functions;
using Playground.Router;
using Playground.Router.Clients;
using Playground.Router.Clients.File;
using Playground.Router.Services;

namespace Playground.PaymentEngine.Api.Setup {
    public static class RoutingSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<Playground.Router.Engine, RouterEngine>();
            builder.Services.AddSingleton<IRoutingService, RoutingService>();
            builder.Services.AddSingleton<ClientFactory, DefaultClientFactory>();
            builder.Services.AddSingleton<HttpTerminalClient>();
            builder.Services.AddSingleton<FileTerminalClient>();
            builder.Services.AddSingleton<XsltExtensions, CustomExtensions>();
        }
    }
}