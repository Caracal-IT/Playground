using System.Collections.Generic;

namespace Playground.PaymentEngine.Api.Terminals.Functions {
    public class CustomExtensions: XsltExtensions {
        public Dictionary<string, object> GetExtensions() {
            return new Dictionary<string, object> {
                { "utility:hashing/v1", new HashFunctions() }
            };
        }
    }
}