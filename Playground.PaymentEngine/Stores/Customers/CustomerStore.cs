using Playground.PaymentEngine.Stores.Customers.Model;

namespace Playground.PaymentEngine.Stores.Customers {
    public interface CustomerStore {
        Customer GetCustomer(long id);
    }
}