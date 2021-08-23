using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Playground.Core.Events;
using Playground.PaymentEngine.Events;
using Playground.PaymentEngine.Helpers;
using Playground.PaymentEngine.Services.CacheService;
using Playground.PaymentEngine.Services.Routing;
using Playground.PaymentEngine.Stores;
using Playground.PaymentEngine.Stores.AccountStores;
using Playground.PaymentEngine.Stores.AccountStores.File;
using Playground.PaymentEngine.Stores.AllocationStores;
using Playground.PaymentEngine.Stores.AllocationStores.File;
using Playground.PaymentEngine.Stores.CustomerStores;
using Playground.PaymentEngine.Stores.CustomerStores.File;
using Playground.PaymentEngine.Stores.PaymentStores;
using Playground.PaymentEngine.Stores.TerminalStores;
using Playground.PaymentEngine.Stores.TerminalStores.File;
using Playground.PaymentEngine.Terminals.Functions;
using Playground.PaymentEngine.UseCases.Payments.AutoAllocate;
using Playground.PaymentEngine.UseCases.Payments.Callback;
using Playground.PaymentEngine.UseCases.Payments.Process;
using Playground.PaymentEngine.UseCases.Payments.RunApprovalRules;
using Playground.Router;
using Playground.Router.Clients;
using Playground.Router.Clients.File;
using Playground.Rules;
using Playground.Rules.CustomActions.Terminal;
using Engine = Playground.Router.Engine;

namespace Playground.PaymentEngine {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services) {
            services.AddLogging();
            
            IFileProvider physicalProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            services.AddSingleton(physicalProvider);
            services.AddSingleton<RuleStore, FileRuleStore>();
            services.AddSingleton<Rules.Engine>();
            
            
            services.AddSingleton<XsltExtensions, CustomExtensions>();
            
            
            services.AddSingleton<PaymentStore, FilePaymentStore>();
            services.AddSingleton<AccountStore, FileAccountStore>();
            services.AddSingleton<AllocationStore, FileAllocationStore>();
            services.AddSingleton<CustomerStore, FileCustomerStore>();
            services.AddSingleton<TerminalStore, FileTerminalStore>();
            
            
            
            
            services.AddSingleton<Engine, RouterEngine>();
            
            services.AddSingleton<ClientFactory, DefaultClientFactory>();
            services.AddSingleton<HttpTerminalClient>();
            services.AddSingleton<FileTerminalClient>();
            
            services.AddSingleton<ProcessUseCase>();
            services.AddSingleton<CallbackUseCase>();
            services.AddSingleton<AutoAllocateUseCase>();
            
            services.AddSingleton<RunApprovalRulesUseCase>();
            services.AddSingleton<TerminalAction>();
            
            services.AddSingleton<EventHub, WebEventHub>();
            
            services.AddSingleton<IRoutingService, RoutingService>();
            services.AddSingleton<ICacheService, CacheService>();

            services.AddControllers();
            services.AddHttpClient();

            services.AddMemoryCache();
           
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