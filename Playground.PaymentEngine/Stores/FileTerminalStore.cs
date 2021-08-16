using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.Router;

using Playground.PaymentEngine.Extensions;
using Playground.PaymentEngine.Model;
using Playground.Router.Old;
using Setting = Playground.Router.Old.Setting;

namespace Playground.PaymentEngine.Stores {
    public class FileTerminalStore: TerminalStore {
        private readonly Store _store;

        public FileTerminalStore() =>
            _store = new FilePaymentStore().GetStore();

        public async Task<IEnumerable<OldTerminal>> GetTerminalsAsync(IEnumerable<string> terminals, CancellationToken cancellationToken) {
            var tasks = terminals.Select(t => GetTerminalAsync(t, cancellationToken));
            return await Task.WhenAll(tasks);
        }
        
        private async Task<OldTerminal> GetTerminalAsync(string name, CancellationToken cancellationToken) {
            var path = Path.Join("Terminals", "Templates", $"{name}.xslt");
            
            if(!File.Exists(path))
                return new OldTerminal{ Name = name, Xslt = string.Empty};

            var terminal = _store.Terminals.TerminalList.FirstOrDefault(t => t.Name == name);

            if (terminal != null) {
                return new OldTerminal {
                    Name = terminal.Name,
                    Type = terminal.Type,
                    RetryCount = terminal.RetryCount,
                    Xslt = await path.ReadFromFileAsync(cancellationToken),
                    Settings = terminal.Settings
                                       .Select(s => new Setting{ Name  = s.Name, Value = s.Value})
                                       .ToList()
                }; 
            }

            return new OldTerminal {
                Name = name,
                Type = "http",
                Xslt = await path.ReadFromFileAsync(cancellationToken),
                RetryCount = 0
            };
        }
    }
}