using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddMvc()
       .AddXmlSerializerFormatters()
       .AddXmlDataContractSerializerFormatters();

builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Playground PaymentEngine Mocks Api", Version = "v1" }); });

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", () => "Hello World!");

if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Playground PaymentEngine Api v1"));
}
            
app.UseHttpsRedirection();
app.MapControllers();
app.Run();