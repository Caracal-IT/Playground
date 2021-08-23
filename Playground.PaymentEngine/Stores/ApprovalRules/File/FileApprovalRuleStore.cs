using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;

namespace Playground.PaymentEngine.Stores.ApprovalRules.File {
    public class FileApprovalRuleStore: ApprovalRuleStore {
        private readonly ApprovalRuleRepository _repository;

        public FileApprovalRuleStore() =>
            _repository = GetRepository();
        
        public Task<IEnumerable<ApprovalRuleHistory>> GetRuleHistoriesAsync(IEnumerable<long> withdrawalGroupIds, CancellationToken cancellationToken) {
            var result = _repository.ApprovalRuleRuleHistories
                                    .Where(h => withdrawalGroupIds.Contains(h.WithdrawalGroupId));

            return Task.FromResult(result);
        }

        public Task AddRuleHistoriesAsync(IEnumerable<ApprovalRuleHistory> histories, CancellationToken cancellationToken) {
            _repository.ApprovalRuleRuleHistories.AddRange(histories);

            return Task.CompletedTask;
        }

        private static ApprovalRuleRepository GetRepository() {
            var path = Path.Join("Stores", "ApprovalRules", "File", "repository.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            return (ApprovalRuleRepository) new XmlSerializer(typeof(ApprovalRuleRepository)).Deserialize(fileStream);
        }
    }
}