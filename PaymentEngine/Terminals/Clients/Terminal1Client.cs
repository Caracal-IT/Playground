using System.Threading.Tasks;
using System.Xml.Serialization;
using Router.Clients;
using static Router.Helpers.Serializer;

namespace PaymentEngine.Terminals.Clients {
    public class Terminal1Client: Client {
        public Task<string> SendAsync(string message) {
            if (message.Contains("<request")) {
                var request = DeSerialize<Terminal1Request>(message);

                var response = new Terminal1Response {
                    Name = $"Terminal 1 - {request!.CardHolder}, Hash - {request!.Hash}",
                    TransactionRef = request!.TransactionRef,
                    Amount = request.Amount
                };

                return Task.FromResult(Serialize(response));
            }
            
            if (message.Contains("<callback-request")) {
                
                return Task.FromResult($"<callback-response>{message}</callback-response>");
            }

            return Task.FromResult("<XmlData/>");
        }
    }
    
    [XmlRoot("callback-request")]
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