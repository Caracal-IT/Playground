using Playground.PaymentEngine.Stores.CustomerStores.Model;

namespace Playground.PaymentEngine.Stores.CustomerStores {
    public interface CustomerStore {
        Customer GetCustomer(long id);
    }
}