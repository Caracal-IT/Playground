using System.Threading.Tasks;
using System.Xml.Serialization;
using Playground.Router;
using Playground.Router.Clients;

using static Playground.Xml.Serialization.Serializer;

namespace Playground.MockTerminals {
    public class Terminal3Client: Client {
        public async Task<string> SendAsync(Configuration configuration, string message, Terminal terminal) {
            await Task.Delay(0);
            
            var request = DeSerialize<Terminal3Request>(message);
            
            var response = new Terminal3Response {
                Name = "Terminal 3",
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
        public string? TransactionRef { get; set; }
        [XmlElement("amount")]
        public decimal Amount { get; set; }
        
        [XmlElement("code")]
        public string? Code { get; set; }
    }

    [XmlRoot("response")]
    public class Terminal3Response {
        [XmlAttribute("name")] public string Name { get; set; } = nameof(Terminal3Response);
        
        [XmlElement("amount")]
        public decimal Amount { get; set; }
        
        [XmlElement("trans-ref")]
        public string? TransactionRef { get; set; }
        
        [XmlElement("code")]
        public string? Code { get; set; }
    }
}