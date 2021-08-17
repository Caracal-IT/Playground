using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Extensions;
using Playground.PaymentEngine.Stores;
using Playground.PaymentEngine.Terminals.Functions;
using Playground.Router;
using Playground.Xml.Serialization;

namespace Playground.PaymentEngine.Services.Routing {
    public record RoutingRequest(Guid TransactionId, string Name, string Payload, IEnumerable<string> Terminals);
    
    public class RoutingService: IRoutingService {
        private readonly PaymentStore _paymentStore;
        private readonly Engine _engine;
        private readonly Dictionary<string, object> _extensions;

        private IEnumerable<Terminal> _terminals;

        public RoutingService(PaymentStore paymentStore, Engine engine, XsltExtensions extensions) {
            _engine = engine;
            _paymentStore = paymentStore;
            _extensions = extensions.GetExtensions();
        }

        public async Task<IEnumerable<Response>> Send(RoutingRequest request, CancellationToken cancellationToken) {
            await InitTerminals(cancellationToken);
            
            var req = new Request { 
                TransactionId = request.TransactionId,
                Name = request.Name,
                Payload = request.Payload,
                Extensions = _extensions,
                Terminals = _terminals.Where(t => request.Terminals.Contains(t.Name))
            };

            return await _engine.ProcessAsync(req, cancellationToken);
        }

        private async Task InitTerminals(CancellationToken cancellationToken) {
            if (_terminals != null)
                return;
            
            var terminals = _paymentStore.GetTerminals().Select(async t => new Terminal {
                Name = t.Name,
                Settings = t.Settings.Select(s => new Setting(s.Name, s.Value)),
                Type = t.Type,
                RetryCount = t.RetryCount,
                Xslt = await GetXslt(t.Name)
            });

            _terminals = await Task.WhenAll(terminals);

            async Task<string> GetXslt(string name) {
                var path = Path.Join("Terminals", "Templates", $"{name}.xslt");

                if (!File.Exists(path)) return string.Empty;

                return await path.ReadFromFileAsync(cancellationToken);
            }   
        }



    }
}