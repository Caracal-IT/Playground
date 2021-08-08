using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentEngine.Extensions {
    public static class StringExtensions {
        public static async Task<string> ReadFromFileAsync(this string path, CancellationToken token) {
            using var sr = new StreamReader(path);
            return await sr.ReadToEndAsync();
        }
    }
}