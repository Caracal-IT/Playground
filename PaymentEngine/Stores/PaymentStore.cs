using System.IO;
using System.Xml.Serialization;
using PaymentEngine.Model;

namespace PaymentEngine.Stores {
    public interface PaymentStore {
        Store GetStore();
    }
}