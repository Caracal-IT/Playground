using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.Router;

using Playground.PaymentEngine.Extensions;
using Playground.PaymentEngine.Model;
using Setting = Playground.Router.Setting;
using Terminal = Playground.Router.Terminal;

namespace Playground.PaymentEngine.Stores {
    public class FileTerminalStore: TerminalStore {
        private readonly Store _store;

        public FileTerminalStore() =>
            _store = new FilePaymentStore().GetStore();

        public async Task<IEnumerable<Terminal>> GetTerminalsAsync(IEnumerable<string> terminals, CancellationToken cancellationToken) {
            var tasks = terminals.Select(t => GetTerminalAsync(t, cancellationToken));
            return await Task.WhenAll(tasks);
        }
        
        private async Task<Terminal> GetTerminalAsync(string name, CancellationToken cancellationToken) {
            var path = Path.Join("Terminals", "Templates", $"{name}.xslt");
            
            if(!File.Exists(path))
                return new Terminal{ Name = name, Xslt = string.Empty};

            var terminal = _store.Terminals.TerminalList.FirstOrDefault(t => t.Name == name);

            if (terminal != null) {
                return new Terminal {
                    Name = terminal.Name,
                    Type = terminal.Type,
                    RetryCount = terminal.RetryCount,
                    Xslt = await path.ReadFromFileAsync(cancellationToken),
                    Settings = terminal.Settings
                                       .Select(s => new Setting{ Name  = s.Name, Value = s.Value})
                                       .ToList()
                }; 
            }

            return new Terminal {
                Name = name,
                Type = "http",
                Xslt = await path.ReadFromFileAsync(cancellationToken),
                RetryCount = 0
            };
        }
    }
}