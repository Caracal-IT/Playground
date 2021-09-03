using Playground.PaymentEngine.Api.Models.Customers;
using CreateCustomer = Playground.PaymentEngine.Application.UseCases.Customers.CreateCustomer;
using GetCustomers = Playground.PaymentEngine.Application.UseCases.Customers.GetCustomers;
using GetCustomer = Playground.PaymentEngine.Application.UseCases.Customers.GetCustomer;
using EditCustomer = Playground.PaymentEngine.Application.UseCases.Customers.EditCustomer;

namespace Playground.PaymentEngine.Api.Setup.Profiles {
    public class CustomerProfile: Profile {
        public CustomerProfile() {
            CreateMap<CreateCustomerRequest, CreateCustomer.CreateCustomerRequest>();
            CreateMap<CreateCustomer.Customer, Customer>();
            CreateMap<GetCustomers.Customer, Customer>();
            CreateMap<GetCustomer.Customer, Customer>();
            CreateMap<EditCustomer.EditCustomerRequest, EditCustomerRequest>();
            CreateMap<EditCustomer.MetaDataUpdate, MetaDataUpdate>();
            CreateMap<EditCustomer.Customer, Customer>();
        }
    }
}