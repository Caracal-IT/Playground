using Data = Playground.PaymentEngine.Store.Customers.Model;
using SharedData = Playground.PaymentEngine.Store.Model;

namespace Playground.PaymentEngine.Application.UseCases.Customers.EditCustomer {
    public class CustomerProfile: Profile {
        public CustomerProfile() {
            CreateMap<Data.Customer, Customer>();
            CreateMap<MetaDataUpdate, SharedData.MetaData>();
        }
    }
}