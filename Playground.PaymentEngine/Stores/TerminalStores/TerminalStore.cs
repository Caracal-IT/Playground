using System.Collections.Generic;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores.TerminalStores.Model;

namespace Playground.PaymentEngine.Stores.TerminalStores {
    public interface TerminalStore {
        IEnumerable<Terminal> GetActiveAccountTypeTerminals(long accountTypeId);
        IEnumerable<Terminal> GetTerminals();
        void LogTerminalResults(IEnumerable<TerminalResult> results);
    }
}