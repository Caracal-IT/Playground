using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.Accounts;
using Playground.PaymentEngine.Stores.Allocations;
using Playground.PaymentEngine.Stores.Withdrawals.Model;

namespace Playground.PaymentEngine.Stores.Withdrawals.File {
    public class FileWithdrawalStore: WithdrawalStore {
        private readonly WithdrawalRepository _repository;

        public FileWithdrawalStore(AccountStore accountStore, AllocationStore allocationStore) => 
            _repository = GetRepository();

        public IEnumerable<Withdrawal> GetWithdrawals(IEnumerable<long> withdrawalIds) =>
            _repository.Withdrawals
                       .Where(w => withdrawalIds.Contains(w.Id));
        
        public IEnumerable<Withdrawal> GetWithdrawalGroupWithdrawals(long id) {
            var group = _repository.WithdrawalGroups.FirstOrDefault(g => g.Id == id)??new WithdrawalGroup();
            return GetWithdrawals(group.WithdrawalIds);
        }
        public IEnumerable<WithdrawalGroup> GetWithdrawalGroups(IEnumerable<long> withdrawalGroupIds) => 
            _repository.WithdrawalGroups
                       .Where(g => withdrawalGroupIds.Contains(g.Id));
        
        public WithdrawalGroup GetWithdrawalGroup(long id) =>
            _repository.WithdrawalGroups
                       .FirstOrDefault(g => g.Id == id);
        
        private static WithdrawalRepository GetRepository() {
            var path = Path.Join("Stores", "Withdrawals", "File", "repository.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            return (WithdrawalRepository) new XmlSerializer(typeof(WithdrawalRepository)).Deserialize(fileStream);
        }
    }
}