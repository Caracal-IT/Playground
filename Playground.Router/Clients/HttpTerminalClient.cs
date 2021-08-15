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
        
        public async Task<string> SendAsync(Configuration configuration, string message, Terminal terminal, CancellationToken cancellationToken) {
            if (configuration.Settings.All(s => s.Name != "url"))
                return "<XmlData/>";

            var url = configuration.Settings.First(s => s.Name == "url").Value;
            dynamic requestJson = JObject.Parse(message.ToJson()!.ToString());
            var req = requestJson.request.ToString();

            var content = new StringContent(req, UTF8, "application/json");

            var resp = await _httpClient.PostAsync(url, content, cancellationToken);
            var result = await resp.Content.ReadAsStringAsync(cancellationToken);
            return result.ToXml("response").ToString(SaveOptions.None);
        }
    }
}