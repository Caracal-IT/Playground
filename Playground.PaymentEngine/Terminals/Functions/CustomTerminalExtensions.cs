using System.Collections.Generic;
using Playground.Router;

namespace Playground.PaymentEngine.Terminals.Functions {
    public class CustomTerminalExtensions: TerminalExtensions {
        public Dictionary<string, object> GetExtensions() {
            return new Dictionary<string, object> {
                { "utility:hashing/v1", new HashFunctions() }
            };
        }
    }
}