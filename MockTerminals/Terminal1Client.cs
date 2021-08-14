using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Router;
using Router.Clients;
using static Router.Helpers.Serializer;

namespace MockTerminals {
    public class Terminal1Client: Client {
        public async Task<string> SendAsync(Configuration configuration, string message, Terminal terminal) {
            var requestType = configuration.Settings.FirstOrDefault(s => s.Name == "req-type")?.Value??string.Empty;
            
            switch (requestType) {
                case "process": {
                    var request = DeSerialize<Terminal1Request>(message);
                    var usr = terminal.Settings.FirstOrDefault(s => s.Name == "auth-user")?.Value ?? "";

                    var response = new Terminal1Response {
                        Name = $"User : {usr} Terminal 1 - {request!.CardHolder}, Hash - {request!.Hash}",
                        TransactionRef = request!.TransactionRef,
                        Amount = request.Amount,
                        Code = request.Code
                    };

                    if (request.Code == "10")
                        throw new Exception("test");

                    return await Task.FromResult(Serialize(response));
                }
                case "callback":
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
        
        [XmlElement("code")]
        public string Code { get; set; }
    }

    [XmlRoot("response")]
    public class Terminal1Response {
        [XmlAttribute("name")] public string Name { get; set; } = nameof(Terminal1Response);
        
        [XmlElement("trans-ref")]
        public string TransactionRef { get; set; }
        
        [XmlElement("amount")]
        public decimal Amount { get; set; }
        [XmlElement("code")]
        public string Code { get; set; }
    }
}