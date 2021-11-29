// ReSharper disable InconsistentNaming
namespace Playground.PaymentEngine.Store.EF.Terminals; 

public partial class EFTerminalStore: DbContext, TerminalStore {
    public Task<IEnumerable<Terminal>> GetActiveAccountTypeTerminalsAsync(long accountTypeId, CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Terminal>> GetTerminalsAsync(CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }

    public Task LogTerminalResultsAsync(IEnumerable<TerminalResult> results, CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }
}