using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Router;

using PaymentEngine.Extensions;

namespace PaymentEngine.Stores {
    public class FileTerminalStore: TerminalStore {
        public async Task<Terminal> GetTerminalAsync(string name, CancellationToken token) {
            var path = Path.Join("Resources", "Templates", "Terminals", $"{name}.xslt");
            
            if(!File.Exists(path))
                return new Terminal{ Name = name, Xslt = string.Empty};

            return new Terminal {
                Name = name,
                Xslt = await path.ReadFromFileAsync(token)
            };
        }
    }
}