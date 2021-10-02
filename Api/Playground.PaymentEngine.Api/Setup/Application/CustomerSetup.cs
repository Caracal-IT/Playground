namespace Playground.PaymentEngine.Api.Setup.Application;

using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.Application.UseCases.Customers.CreateCustomer;
using Playground.PaymentEngine.Application.UseCases.Customers.DeleteCustomer;
using Playground.PaymentEngine.Application.UseCases.Customers.EditCustomer;
using Playground.PaymentEngine.Application.UseCases.Customers.GetCustomer;
using Playground.PaymentEngine.Application.UseCases.Customers.GetCustomers;

public static class CustomerSetup {
    public static void Setup(WebApplicationBuilder builder) {
        builder.Services.AddSingleton<CreateCustomerUseCase>();
        builder.Services.AddSingleton<GetCustomersUseCase>();
        builder.Services.AddSingleton<GetCustomerUseCase>();
        builder.Services.AddSingleton<DeleteCustomerUseCase>();
        builder.Services.AddSingleton<EditCustomerUseCase>();
    }
}