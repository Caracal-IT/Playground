using Data = Playground.PaymentEngine.Store.Customers.Model;

namespace Playground.PaymentEngine.Application.UseCases.Customers.GetCustomers {
    public class CustomerProfile: Profile {
        public CustomerProfile() {
            CreateMap<Data.Customer, Customer>();
        }
    }
}