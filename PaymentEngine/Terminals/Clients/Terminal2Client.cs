using System.Threading.Tasks;
using System.Xml.Serialization;
using Router.Clients;
using static Router.Helpers.Serializer;

namespace PaymentEngine.Terminals.Clients {
    public class Terminal2Client: Client {
        public async Task<string> SendAsync(string message, int requestType, string name = "") {
            await Task.Delay(0);
            
            var request = DeSerialize<Terminal2Request>(message);
            
            var response = new Terminal2Response {
                TransactionRef = request!.TransactionRef
            };
            
            return Serialize(response);
        }
    }

    [XmlRoot("request")]
    public class Terminal2Request {
        [XmlElement("trans-ref")]
        public string TransactionRef { get; set; }
    }

    [XmlRoot("response")]
    public class Terminal2Response {
        [XmlAttribute("name")] public string Name { get; set; } = nameof(Terminal2Response);
        
        [XmlElement("trans-ref")]
        public string TransactionRef { get; set; }
    }
}