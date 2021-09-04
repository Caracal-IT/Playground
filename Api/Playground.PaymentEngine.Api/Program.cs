using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Playground.PaymentEngine.Application.UseCases.Shared;

var builder = WebApplication.CreateBuilder(args);

ApiSetup.Setup(builder);
InfrastructureSetup.Setup(builder);

builder.Services.AddAutoMapper(typeof(ApiSetup).Assembly, typeof(SharedProfile).Assembly);

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

builder.Services.AddLogging();
builder.Services.AddHttpClient();
builder.Services.AddControllers()
                .AddOData(options => options.EnableQueryFeatures());

builder.Services.AddMvc()
                .AddXmlSerializerFormatters()
                .AddXmlDataContractSerializerFormatters();

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Playground PaymentEngine Api", Version = "v1" }); });

var app = builder.Build();

ApiSetup.Register(app);

if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Playground PaymentEngine Api v1"));
}
            
app.UseHttpsRedirection();
app.MapControllers();
app.Run();