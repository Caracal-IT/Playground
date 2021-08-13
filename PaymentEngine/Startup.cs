using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PaymentEngine.Stores;
using PaymentEngine.Terminals.Functions;
using PaymentEngine.UseCases.Payments.Callback;
using PaymentEngine.UseCases.Payments.Process;
using Router;
using Router.Clients;
using MockClientFactory = PaymentEngine.Terminals.Clients.MockClientFactory;

namespace PaymentEngine {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton<TerminalExtensions, CustomTerminalExtensions>();
            services.AddSingleton<TerminalStore, FileTerminalStore>();
            services.AddSingleton<PaymentStore, FilePaymentStore>();
            services.AddSingleton<RouterEngine, Engine>();
            services.AddSingleton<ClientFactory, MockClientFactory>();
            
            services.AddSingleton<ProcessUseCase>();
            services.AddSingleton<CallbackUseCase>();
            
            services.AddControllers();
           
            services.AddMvc()
                .AddXmlSerializerFormatters()
                .AddXmlDataContractSerializerFormatters();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentEngine", Version = "v1" }); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentEngine v1"));
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}