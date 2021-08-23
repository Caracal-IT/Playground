using System.Collections.Generic;
using Playground.PaymentEngine.Stores.Terminals.Model;

namespace Playground.PaymentEngine.Stores.Terminals {
    public interface TerminalStore {
        IEnumerable<Terminal> GetActiveAccountTypeTerminals(long accountTypeId);
        IEnumerable<Terminal> GetTerminals();
        void LogTerminalResults(IEnumerable<TerminalResult> results);
    }
}