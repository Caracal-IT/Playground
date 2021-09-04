using SharedData = Playground.PaymentEngine.Store.Model;

namespace Playground.PaymentEngine.Application.UseCases.Customers.EditCustomer {
    public class CustomerProfile: Profile {
        public CustomerProfile() {
            CreateMap<MetaDataUpdate, SharedData.MetaData>();
        }
    }
}