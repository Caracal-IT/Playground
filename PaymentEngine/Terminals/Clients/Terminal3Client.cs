using System.Threading.Tasks;
using System.Xml.Serialization;
using Router;
using Router.Clients;
using static Router.Helpers.Serializer;

namespace PaymentEngine.Terminals.Clients {
    public class Terminal3Client: Client {
        public async Task<string> SendAsync(Configuration configuration, string message, Terminal terminal, string requestName) {
            await Task.Delay(0);
            
            var request = DeSerialize<Terminal3Request>(message);
            
            var response = new Terminal3Response {
                Name = requestName,
                TransactionRef = request!.TransactionRef,
                Amount = request!.Amount,
                Code = request!.Code
            };
            
            return Serialize(response);
        }
    }

    [XmlRoot("request")]
    public class Terminal3Request {
        [XmlElement("trans-ref")]
        public string TransactionRef { get; set; }
        [XmlElement("amount")]
        public decimal Amount { get; set; }
        
        [XmlElement("code")]
        public string Code { get; set; }
    }

    [XmlRoot("response")]
    public class Terminal3Response {
        [XmlAttribute("name")] public string Name { get; set; } = nameof(Terminal3Response);
        
        [XmlElement("amount")]
        public decimal Amount { get; set; }
        
        [XmlElement("trans-ref")]
        public string TransactionRef { get; set; }
        
        [XmlElement("code")]
        public string Code { get; set; }
    }
}