namespace Playground.PaymentEngine.Application.UseCases.Customers.CreateCustomer;

using Data = Playground.PaymentEngine.Store.Customers.Model;

public class CustomerProfile: Profile {
    public CustomerProfile() {
        CreateMap<CreateCustomerRequest, Data.Customer>();
    }
}