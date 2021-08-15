using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Playground.Xml;
using Playground.Xml.Serialization;
using Playground.XsltTransform;
using Playground.XsltTransform.Extensions;

namespace Playground.Router {
    internal class Transaction<T> where T : class { 
        private readonly Terminal _terminal;
        private readonly TerminalProcessor<T> _processor;

        private Transaction(TerminalProcessor<T> processor, Terminal terminal) {
            _terminal = terminal;
            _processor = processor;
        }

        public static async Task<string?> ProcessAsync<TS>(TerminalProcessor<TS> processor, Terminal terminal) where TS : class {
            var t = new Transaction<TS>(processor, terminal);
            return await t.ProcessAsync();
        }
            
        private async Task<string?> ProcessAsync() {
            var client = _processor.Factory.Create(_terminal);
            var responseXml = await client.SendAsync(GetConfiguration(), GetClientMessage(), _terminal);

            return ProcessResponseMessage(responseXml);
        }

        private string GetClientMessage() => Transformer.Transform(_processor.RequestXml, _terminal.Xslt!, _processor.Extensions);

        private Configuration GetConfiguration() {
            var configXml = Transformer.Transform($"<request name='{_processor.Request.Name}'><config/></request>", _terminal.Xslt!, _processor.Extensions);
            var config = new Configuration();

            if (!string.IsNullOrWhiteSpace(configXml))
                config = Serializer.DeSerialize<Configuration>(configXml) ?? new Configuration();

            return config;
        }

        private string? ProcessResponseMessage(string response) {
            var xml = WrapXmlInResponseTag(response).Transform(_terminal.Xslt!, _processor.Extensions)
                .ToXml();

            if (HasFailed(xml!))
                return null;

            return xml!.FirstNode?.ToString() ?? string.Empty;
        }

        private string WrapXmlInResponseTag(string xmlStr) =>
            $"<request name='{_processor.Request.Name}'>{xmlStr.ToXml()}</request>";

        private static bool HasFailed(XContainer xml) {
            if (xml?.Nodes().Count() > 1)
                return !Serializer.DeSerialize<Response>(xml.LastNode!.ToString())?.Success ?? true;

            return false;
        }
    }
}