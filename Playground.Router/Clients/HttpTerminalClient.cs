using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Playground.Xml;
using static System.Text.Encoding;

namespace Playground.Router.Clients {
    public class HttpTerminalClient: Client {
        private readonly HttpClient _httpClient;
        
        public HttpTerminalClient(HttpClient httpClient) =>
            _httpClient = httpClient;
        
        public async Task<string> SendAsync(Configuration configuration, string message, CancellationToken cancellationToken) {
            if (configuration.Settings.All(s => s.Name != "url"))
                return "<XmlData/>";

            var url = configuration.Settings.First(s => s.Name == "url").Value;
            var contentType = configuration.Settings.FirstOrDefault(s => s.Name == "content-type")?.Value.ToLower() ?? "application/json";
            var isXml = contentType == "application/xml";
            
            string request = isXml ? message : GetJsonText();
            var content = new StringContent(request, UTF8, contentType);

            configuration.Settings
                        .Where(s => s.Name.StartsWith("header:"))
                        .ToList()
                        .ForEach(s => content.Headers.Add(s.Name[7..], new[]{s.Value}));

            var resp = await _httpClient.PostAsync(url, content, cancellationToken);
            var result = await resp.Content.ReadAsStringAsync(cancellationToken);
            
            return isXml ? result : JsonResponseToXml();
            
            string GetJsonText() {
                dynamic requestJson = JObject.Parse(message.ToJson()!.ToString());
                return requestJson.request.ToString();
            }

            string JsonResponseToXml() =>
                result.ToXml("response").ToString(SaveOptions.None);
        }
    }
}