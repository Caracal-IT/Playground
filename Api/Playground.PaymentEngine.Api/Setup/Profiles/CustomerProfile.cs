using Playground.PaymentEngine.Api.Models.Customers;
using Playground.PaymentEngine.Api.Models.Shared;
using CreateCustomer = Playground.PaymentEngine.Application.UseCases.Customers.CreateCustomer;
using GetCustomers = Playground.PaymentEngine.Application.UseCases.Customers.GetCustomers;
using Customers = Playground.PaymentEngine.Application.UseCases.Customers;
using EditCustomer = Playground.PaymentEngine.Application.UseCases.Customers.EditCustomer;
using Shared = Playground.PaymentEngine.Application.UseCases.Shared;

namespace Playground.PaymentEngine.Api.Setup.Profiles {
    public class CustomerProfile: Profile {
        public CustomerProfile() {
            CreateMap<CreateCustomerRequest, CreateCustomer.CreateCustomerRequest>();
            CreateMap<PaymentEngine.Application.UseCases.Customers.Customer, Customer>();
            CreateMap<Customers.Customer, Customer>();
            CreateMap<EditCustomerRequest, EditCustomer.EditCustomerRequest>();
            CreateMap<MetaDataUpdate, EditCustomer.MetaDataUpdate>();
            CreateMap<MetaData, Shared.MetaData>();
        }
    }
}