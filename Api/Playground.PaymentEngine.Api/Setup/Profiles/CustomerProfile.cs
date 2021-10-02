namespace Playground.PaymentEngine.Api.Setup.Profiles;

using Models.Shared;

using CreateCustomer = Playground.PaymentEngine.Application.UseCases.Customers.CreateCustomer;
using Customers = Playground.PaymentEngine.Application.UseCases.Customers;
using EditCustomer = Playground.PaymentEngine.Application.UseCases.Customers.EditCustomer;
using Shared = Playground.PaymentEngine.Application.UseCases.Shared;

using ViewModel = Models.Customers;

public class CustomerProfile: Profile {
    public CustomerProfile() {
        CreateMap<ViewModel.CreateCustomerRequest, CreateCustomer.CreateCustomerRequest>();
        CreateMap<Customers.Customer, ViewModel.Customer>();
        CreateMap<Customers.Customer, ViewModel.Customer>();
        CreateMap<ViewModel.EditCustomerRequest, EditCustomer.EditCustomerRequest>();
        CreateMap<ViewModel.MetaDataUpdate, EditCustomer.MetaDataUpdate>();
        CreateMap<MetaData, Shared.MetaData>();
    }
}