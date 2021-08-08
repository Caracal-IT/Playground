using System.IO;
using System.Xml.Serialization;
using PaymentEngine.Model;

namespace PaymentEngine.Stores {
    public class FilePaymentStore: PaymentStore {
        private Store _store;

        public FilePaymentStore() => LoadStore();

        public Store GetStore() => _store;
        
        private void LoadStore() {
            var path = Path.Join("Resources", "Data", "store.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            _store = (Store) new XmlSerializer(typeof(Store)).Deserialize(fileStream);
        }
    }
}