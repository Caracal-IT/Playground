using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;
using Playground.Router;
using Playground.Router.Clients;
using Playground.Xml;
using static System.Text.Encoding;
using static Playground.Xml.Serialization.Serializer;

namespace Playground.MockTerminals {
    public class RBProcessRequest {
        public string Reference { get; set; }
    }

    public class RBProcessResponse {
        public string Reference { get; set; }
        public string Code { get; set; }
    }
    
    public class Terminal1Client: Client {
        private static HttpClient _httpClient = new HttpClient();
        
        public async Task<string> SendAsync(Configuration configuration, string message, Terminal terminal) {
            var requestType = configuration.Settings.FirstOrDefault(s => s.Name == "req-type")?.Value??string.Empty;

            switch (requestType) {
                case "process": {
                    if (configuration.Settings.All(s => s.Name != "url"))
                        return "<XmlData/>";
                    
                    var url = configuration.Settings.First(s => s.Name == "url").Value;
                    dynamic requestJson = JObject.Parse(message.ToJson()!.ToString());
                    string req = requestJson.request.ToString();
                    
                    var resp = await _httpClient.PostAsync(url, new StringContent(req, UTF8, "application/json"));
                    var result = await resp.Content.ReadAsStringAsync();
                    return result.ToXml("response").ToString(SaveOptions.None);
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
        public string? Reference { get; set; }
        [XmlAttribute("code")]
        public string? Code { get; set; }
    }

    [XmlRoot("request")]
    public class Terminal1Request {
        [XmlElement("trans-ref")]
        public string? TransactionRef { get; set; }
        
        [XmlElement("amount")]
        public decimal Amount { get; set; }
        
        [XmlElement("card-holder")]
        public string? CardHolder { get; set; }
        
        [XmlElement("hash")]
        public string? Hash { get; set; }
        
        [XmlElement("code")]
        public string? Code { get; set; }
    }

    [XmlRoot("response")]
    public class Terminal1Response {
        [XmlAttribute("name")] public string Name { get; set; } = nameof(Terminal1Response);
        
        [XmlElement("trans-ref")]
        public string? TransactionRef { get; set; }
        
        [XmlElement("amount")]
        public decimal Amount { get; set; }
        [XmlElement("code")]
        public string? Code { get; set; }
    }
}