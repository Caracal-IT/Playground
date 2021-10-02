namespace Playground.PaymentEngine.Application.UseCases.Customers.EditCustomer;

using SharedData = Playground.PaymentEngine.Store.Model;

public class CustomerProfile: Profile {
    public CustomerProfile() {
        CreateMap<MetaDataUpdate, SharedData.MetaData>();
    }
}