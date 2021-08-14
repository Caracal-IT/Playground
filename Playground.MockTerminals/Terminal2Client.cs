using System.Threading.Tasks;
using System.Xml.Serialization;
using Playground.Router;
using Playground.Router.Clients;

using static Playground.Xml.Serialization.Serializer;

namespace Playground.MockTerminals {
    public class Terminal2Client: Client {
        public async Task<string> SendAsync(Configuration configuration, string message, Terminal terminal) {
            await Task.Delay(0);
            
            var request = DeSerialize<Terminal2Request>(message);
            
            var response = new Terminal2Response {
                Name = "Terminal 2",
                TransactionRef = request!.TransactionRef
            };
            
            return Serialize(response);
        }
    }

    [XmlRoot("request")]
    public class Terminal2Request {
        [XmlElement("trans-ref")]
        public string? TransactionRef { get; set; }
    }

    [XmlRoot("response")]
    public class Terminal2Response {
        [XmlAttribute("name")] public string Name { get; set; } = nameof(Terminal2Response);
        
        [XmlElement("trans-ref")]
        public string? TransactionRef { get; set; }
    }
}