using System.Threading.Tasks;
using System.Xml.Serialization;
using Router.Clients;
using static Router.Helpers.Serializer;

namespace PaymentEngine.Terminals.Clients {
    public class Terminal3Client: Client {
        public async Task<string> SendAsync(string message) {
            await Task.Delay(0);
            
            var request = DeSerialize<Terminal3Request>(message);
            
            var response = new Terminal3Response {
                TransactionRef = request!.TransactionRef
            };
            
            return Serialize(response);
        }
    }

    [XmlRoot("request")]
    public class Terminal3Request {
        [XmlElement("trans-ref")]
        public string TransactionRef { get; set; }
    }

    [XmlRoot("response")]
    public class Terminal3Response {
        [XmlAttribute("name")] public string Name { get; set; } = nameof(Terminal3Response);
        
        [XmlElement("trans-ref")]
        public string TransactionRef { get; set; }
    }
}