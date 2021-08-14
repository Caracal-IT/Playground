using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Router;

using PaymentEngine.Extensions;
using PaymentEngine.Model;
using Setting = Router.Setting;
using Terminal = Router.Terminal;

namespace PaymentEngine.Stores {
    public class FileTerminalStore: TerminalStore {
        private FilePaymentStore _filePaymentStore;
        private Store _store;

        public FileTerminalStore() {
            _filePaymentStore = new FilePaymentStore();
            _store = _filePaymentStore.GetStore();
        }
        
        public async Task<Terminal> GetTerminalAsync(string name, CancellationToken token) {
            var path = Path.Join("Terminals", "Templates", $"{name}.xslt");
            
            if(!File.Exists(path))
                return new Terminal{ Name = name, Xslt = string.Empty};

            var terminal = _store.Terminals.TerminalList.FirstOrDefault(t => t.Name == name);

            if (terminal != null) {
                return new Terminal {
                    Name = terminal.Name,
                    RetryCount = terminal.RetryCount,
                    Xslt = await path.ReadFromFileAsync(token),
                    Settings = terminal.Settings
                                       .Select(s => new Setting{ Name  = s.Name, Value = s.Value})
                                       .ToList()
                }; 
            }

            return new Terminal {
                Name = name,
                Xslt = await path.ReadFromFileAsync(token)
            };
        }
    }
}