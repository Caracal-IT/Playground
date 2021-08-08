using System.Threading.Tasks;
using System.Xml.Serialization;
using static Router.Helpers.Serializer;

namespace Router.Clients {
    public class Terminal1Client: Client {
        public async Task<string> SendAsync(string message) {
            await Task.Delay(0);
            
            var request = DeSerialize<Terminal1Request>(message);
            
            var response = new Terminal1Response {
                TransactionRef = request!.TransactionRef,
                Amount = request.Amount
            };
            
            return Serialize(response);
        }
    }

    [XmlRoot("request")]
    public class Terminal1Request {
        [XmlElement("trans-ref")]
        public string? TransactionRef { get; set; }
        
        [XmlElement("amount")]
        public decimal Amount { get; set; }
    }

    [XmlRoot("response")]
    public class Terminal1Response {
        [XmlAttribute("name")] public string? Name { get; set; } = nameof(Terminal1Response);
        
        [XmlElement("trans-ref")]
        public string? TransactionRef { get; set; }
        
        [XmlElement("amount")]
        public decimal Amount { get; set; }
    }
}