using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Playground.Xml;
using static System.Text.Encoding;

namespace Playground.Router.Clients {
    public class HttpTerminalClient: Client {
        private static readonly HttpClient HttpClient = new HttpClient();

        public async Task<string> SendAsync(Configuration configuration, string message, Terminal terminal) {
            if (configuration.Settings.All(s => s.Name != "url"))
                return "<XmlData/>";

            var url = configuration.Settings.First(s => s.Name == "url").Value;
            dynamic requestJson = JObject.Parse(message.ToJson()!.ToString());
            var req = requestJson.request.ToString();

            var resp = await HttpClient.PostAsync(url, new StringContent(req, UTF8, "application/json"));
            var result = await resp.Content.ReadAsStringAsync();
            return result.ToXml("response").ToString(SaveOptions.None);
        }
    }
}