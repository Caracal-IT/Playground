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
        builder.Services.AddSingleton<Engine, RouterEngine>();
        builder.Services.AddSingleton<IRoutingService, RoutingService>();
        builder.Services.AddSingleton<ClientFactory, DefaultClientFactory>();
        builder.Services.AddSingleton<HttpTerminalClient>();
        builder.Services.AddSingleton<FileTerminalClient>();
        builder.Services.AddSingleton<XsltExtensions, CustomExtensions>();
    }
}