using System.Threading.Tasks;
using System.Xml.Serialization;
using Router.Clients;
using static Router.Helpers.Serializer;

namespace PaymentEngine.Terminals.Clients {
    public class Terminal1Client: Client {
        public async Task<string> SendAsync(string message, int requestType, string name = "") {
            switch (requestType) {
                case (int) RequestType.Process: {
                    var request = DeSerialize<Terminal1Request>(message);

                    var response = new Terminal1Response {
                        Name = $"Terminal 1 - {request!.CardHolder}, Hash - {request!.Hash}",
                        TransactionRef = request!.TransactionRef,
                        Amount = request.Amount
                    };

                    return await Task.FromResult(Serialize(response));
                }
                case (int) RequestType.Callback:
                    return await Task.FromResult($"<callback-response>{message}</callback-response>");
                default:
                    return await Task.FromResult("<XmlData/>");
            }
        }
    }
    
    [XmlRoot("request")]
    public class Callback {
        [XmlAttribute("reference")]
        public string Reference { get; set; }
        [XmlAttribute("code")]
        public string Code { get; set; }
    }

    [XmlRoot("request")]
    public class Terminal1Request {
        [XmlElement("trans-ref")]
        public string TransactionRef { get; set; }
        
        [XmlElement("amount")]
        public decimal Amount { get; set; }
        
        [XmlElement("card-holder")]
        public string CardHolder { get; set; }
        
        [XmlElement("hash")]
        public string Hash { get; set; }
    }

    [XmlRoot("response")]
    public class Terminal1Response {
        [XmlAttribute("name")] public string Name { get; set; } = nameof(Terminal1Response);
        
        [XmlElement("trans-ref")]
        public string TransactionRef { get; set; }
        
        [XmlElement("amount")]
        public decimal Amount { get; set; }
    }
}