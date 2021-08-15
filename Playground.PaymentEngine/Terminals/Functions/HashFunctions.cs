using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Playground.PaymentEngine.Terminals.Functions {
    public class HashFunctions {
        public static string Hash256(string strToHash) {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(strToHash))
                .Aggregate(string.Empty, (current, theByte) => current + theByte.ToString("x2"));
        }
    }
}