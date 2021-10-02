namespace Playground.PaymentEngine.Application.UseCases.Customers;

using Data = Playground.PaymentEngine.Store.Customers.Model;

public class CustomerProfile: Profile {
    public CustomerProfile() {
        CreateMap<Data.Customer, Customer>();
    }
}