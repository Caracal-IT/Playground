using Data = Playground.PaymentEngine.Store.Customers.Model;

namespace Playground.PaymentEngine.Application.UseCases.Customers.EditCustomer {
    public class CustomerProfile: Profile {
        public CustomerProfile() {
            CreateMap<Data.Customer, GetCustomer.Customer>();
        }
    }
}