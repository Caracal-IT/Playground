using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Router.Clients;
using XsltTransform;

using static Router.Helpers.Serializer;

namespace Router {
    public class Engine: RouterEngine {
        private readonly TerminalStore _store;
        private readonly ClientFactory _factory;
        private readonly Dictionary<string, object> _extensions;

        public Engine(TerminalStore store, TerminalExtensions extensions, ClientFactory factory) {
            _store = store;
            _factory = factory;
            _extensions = extensions.GetExtensions();
        }

        public async Task<List<string>> ProcessAsync<T>(Request<T> request, CancellationToken token) where T : class {
            var terminals = await Task.WhenAll(request.Terminals.Select(i => GetTerminal(i, token)));
            var requestXml = SerializeRequest();
            
            var response = new List<string>();
            
            foreach (var t in terminals)
                if (await TryProcessAsync(t)) break;
            
            return response;

            string SerializeRequest() {
                var reqStr = Serialize(request);
                var reqXml = XDocument.Parse(reqStr);

                if (!request.Payload.ToString()!.StartsWith("<")) return reqXml.ToString();
                
                var n = reqXml.XPathSelectElement("request/payload");
                n!.RemoveAll();
                n!.Add(XDocument.Parse(request.Payload.ToString()!).Root!);

                return reqXml.ToString();
            }

            async Task<bool> TryProcessAsync(Terminal terminal) {
                for (var i = 0; i < terminal.RetryCount; i++) {
                    if (await ProcessRequestAsync(terminal))
                        return true;

                    await Task.Delay(200, token);
                }

                return false;
            }
            
            async Task<bool> ProcessRequestAsync(Terminal terminal) {
                try {
                    var message = Transformer.Transform(requestXml, terminal.Xslt!, _extensions);
                    var configXml = Transformer.Transform($"<request name='{request.Name}'><config/></request>", terminal.Xslt!, _extensions);
                    var config = new Configuration();

                    if (!string.IsNullOrWhiteSpace(configXml))
                        config = DeSerialize<Configuration>(configXml) ?? new Configuration();

                    var client = _factory.Create(terminal.Name);
                    var resp = await client.SendAsync(config, message, terminal, request.Name);

                    var xDocument = XDocument.Parse(resp);
                    var xml = XDocument.Parse($"<request name='{request.Name}' />");
                    xml.Root!.Add(xDocument.Root);
                    xDocument =  XDocument.Parse(Transformer.Transform(xml.ToString(), terminal.Xslt!, _extensions));

                    if (xDocument.Root!.Nodes().Count() > 1) {
                        var results = DeSerialize<Response>(xDocument.Root!.LastNode!.ToString());
                        
                        if(!results!.Success)
                            return results!.Success;
                    }
                    
                    response.Add(xDocument.Root?.FirstNode?.ToString()??"");

                    return true;
                }
                catch {
                    return false;
                }
            }
        }

        private async Task<Terminal> GetTerminal(KeyValuePair<string, int> terminalSetting, CancellationToken token) {
            var (key, value) = terminalSetting;
            var terminal = await _store.GetTerminalAsync(key, token);
            terminal.RetryCount = value;
            
            return terminal;
        }
    }
}