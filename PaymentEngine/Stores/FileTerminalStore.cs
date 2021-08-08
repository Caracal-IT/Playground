using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Router;

using PaymentEngine.Extensions;

namespace PaymentEngine.Stores {
    public class FileTerminalStore: TerminalStore {
        public async Task<Terminal> GetTerminalAsync(string name, CancellationToken token) {
            var path = Path.Join("Resources", "Templates", "Terminals", name);
            
            if(!Directory.Exists(path))
                return new Terminal{ Name = name };

            return new Terminal {
                Name = name,
                InXslt = await Path.Join(path, "InBound.xslt").ReadFromFileAsync(token),
                OutXslt = await Path.Join(path, "OutBound.xslt").ReadFromFileAsync(token)
            };
        }
    }
}