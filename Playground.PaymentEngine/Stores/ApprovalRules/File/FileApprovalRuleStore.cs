using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;

namespace Playground.PaymentEngine.Stores.ApprovalRules.File {
    public class FileApprovalRuleStore: ApprovalRuleStore {
        private readonly ApprovalRuleRepository _repository;

        public FileApprovalRuleStore() =>
            _repository = GetRepository();
        
        public IEnumerable<ApprovalRuleHistory> GetRuleHistories(IEnumerable<long> withdrawalGroupIds) =>
            _repository.ApprovalRuleRuleHistories
                .Where(h => withdrawalGroupIds.Contains(h.WithdrawalGroupId));

        public void AddRuleHistories(IEnumerable<ApprovalRuleHistory> histories) =>
            _repository.ApprovalRuleRuleHistories.AddRange(histories);
        
        private static ApprovalRuleRepository GetRepository() {
            var path = Path.Join("Stores", "ApprovalRules", "File", "repository.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            return (ApprovalRuleRepository) new XmlSerializer(typeof(ApprovalRuleRepository)).Deserialize(fileStream);
        }
    }
}