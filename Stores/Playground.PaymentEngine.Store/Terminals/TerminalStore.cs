namespace Playground.PaymentEngine.Store.Terminals;
    
using Model;

public interface TerminalStore {
    Task<IEnumerable<Terminal>> GetActiveAccountTypeTerminalsAsync(long accountTypeId, CancellationToken cancellationToken);
    Task<IEnumerable<Terminal>> GetTerminalsAsync(CancellationToken cancellationToken);
    Task LogTerminalResultsAsync(IEnumerable<TerminalResult> results, CancellationToken cancellationToken);
}