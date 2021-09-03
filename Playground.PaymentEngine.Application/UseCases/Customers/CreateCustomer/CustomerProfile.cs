using Data = Playground.PaymentEngine.Store.Customers.Model;

namespace Playground.PaymentEngine.Application.UseCases.Customers.CreateCustomer {
    public class CustomerProfile: Profile {
        public CustomerProfile() {
            CreateMap<CreateCustomerRequest, Data.Customer>();
            CreateMap<Data.Customer, Customer>();
        }
    }
}