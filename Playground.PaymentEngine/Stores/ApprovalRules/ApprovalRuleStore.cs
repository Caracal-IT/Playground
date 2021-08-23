using System.Collections.Generic;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;

namespace Playground.PaymentEngine.Stores.ApprovalRules {
    public interface ApprovalRuleStore {
        IEnumerable<ApprovalRuleHistory> GetRuleHistories(IEnumerable<long> withdrawalIds);

        void AddRuleHistories(IEnumerable<ApprovalRuleHistory> histories);
    }
}