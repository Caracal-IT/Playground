using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RulesEngine.Models;

namespace Playground.Rules {
    public interface RuleStore {
        public Task<IEnumerable<WorkflowRules>> GetRulesAsync(string name, CancellationToken cancellationToken);
    }
}