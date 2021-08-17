using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Playground.Core.Events;
using Playground.PaymentEngine.Events;
using Playground.PaymentEngine.Services.Routing;
using Playground.PaymentEngine.Stores;
using Playground.PaymentEngine.Terminals;
using Playground.PaymentEngine.Terminals.Functions;
using Playground.PaymentEngine.UseCases.Payments.Callback;
using Playground.PaymentEngine.UseCases.Payments.Process;
using Playground.Router;
using Playground.Router.Clients;
using Playground.Router.Clients.File;

namespace Playground.PaymentEngine {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton<Terminals.Functions.XsltExtensions, CustomExtensions>();
            services.AddSingleton<PaymentStore, FilePaymentStore>();
            services.AddSingleton<Engine, RouterEngine>();
            
            services.AddSingleton<ClientFactory, DefaultClientFactory>();
            services.AddSingleton<HttpTerminalClient>();
            services.AddSingleton<FileTerminalClient>();
            
            services.AddSingleton<ProcessUseCase>();
            services.AddSingleton<CallbackUseCase>();

            services.AddSingleton<EventHub, WebEventHub>();
            
            services.AddSingleton<IRoutingService, RoutingService>();

            services.AddControllers();
            services.AddHttpClient();
           
            services.AddMvc()
                .AddXmlSerializerFormatters()
                .AddXmlDataContractSerializerFormatters();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Playground.PaymentEngine", Version = "v1" }); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Playground.PaymentEngine v1"));
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}