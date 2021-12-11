namespace Playground.PaymentEngine.Api.Setup;

using Microsoft.Extensions.DependencyInjection;
using Services;
using Terminals.Functions;
using Router;
using Router.Clients;
using Playground.Router.Clients.File;
using Playground.Router.Services;

public static class RoutingSetup {
    public static void Setup(WebApplicationBuilder builder) {
        builder.Services.AddTransient<Engine, RouterEngine>();
        builder.Services.AddTransient<IRoutingService, RoutingService>();
        builder.Services.AddTransient<ClientFactory, DefaultClientFactory>();
        builder.Services.AddTransient<HttpTerminalClient>();
        builder.Services.AddTransient<FileTerminalClient>();
        builder.Services.AddTransient<XsltExtensions, CustomExtensions>();
    }
}