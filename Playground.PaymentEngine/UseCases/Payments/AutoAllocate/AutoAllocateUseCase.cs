using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores;

namespace Playground.PaymentEngine.UseCases.Payments.AutoAllocate {
    public class AutoAllocateUseCase {
        private readonly PaymentStore _store;

        public AutoAllocateUseCase(PaymentStore store) => 
            _store = store;

        public async Task<AutoAllocateResponse> ExecuteAsync(AutoAllocateRequest request, CancellationToken cancellationToken) {
            await Task.Delay(0, cancellationToken);
            request.WithdrawalGroups.ForEach(RemoveAllocations);
            var results = request.WithdrawalGroups.SelectMany(AllocateFunds).ToList();

            return new AutoAllocateResponse{AllocationResults = results};
        }

        private void RemoveAllocations(long withdrawalGroupId) {
            var allocations = _store.GetStore().Allocations;
            
            _store.GetStore()
                .Allocations
                .Where(a => a.WithdrawalGroupId == withdrawalGroupId)
                .ToList()
                .ForEach(a => allocations.Remove(a));
        }

        private List<AutoAllocateResult> AllocateFunds(long withdrawalGroupId) {
            var result = new List<AutoAllocateResult>();
            
            var withdrawalGroup = _store.GetWithdrawalGroup(withdrawalGroupId);
            var withdrawals = _store.GetWithdrawalGroupWithdrawals(withdrawalGroupId);
            var withdrawalAmount = withdrawals.Sum(w => w.Amount);

            if (withdrawalAmount <= 0M)
                return new List<AutoAllocateResult>();

            var customer = _store.GetCustomer(withdrawalGroup.CustomerId);
            var accounts = _store.GetCustomerAccounts(customer.Id).ToList();
            var accountTypes = _store.GetAccountTypes(accounts.Select(a => a.AccountTypeId));

            var orderedAccounts = accounts.Join(
                    accountTypes, 
                    a => a.AccountTypeId, 
                    t => t.Id, 
                    (a, t) => new {Account = a, Order = t.ProcessOrder}
                )
                .OrderBy(a => a.Order)
                .ThenBy(a => a.Account.IsPreferredAccount)
                .Select(a => a.Account)
                .ToList();
            
            foreach (var account in orderedAccounts.Where(a => a.Exposure > 0)) {
                var amount = withdrawalAmount - account.Exposure >= 0 ? account.Exposure : withdrawalAmount;
                CreateAllocation(account, amount);
                withdrawalAmount -= amount;
                
                if(withdrawalAmount <= 0)
                    break;
            }

            if (withdrawalAmount <= 0) return result;
            
            var preferredAcc = orderedAccounts.FirstOrDefault(a => a.IsPreferredAccount) ?? orderedAccounts.First();
            CreateAllocation(preferredAcc, withdrawalAmount);
            
            return result;
            
            void CreateAllocation(Account account, decimal amount) {
                 var allocation  = _store.SaveAllocation(new Allocation {
                    AccountId = account.Id,
                    Amount = amount,
                    AllocationStatusId = 1,
                    WithdrawalGroupId = withdrawalGroupId
                 });
                 
                 result.Add(MapResult(allocation));
            }
        }

        private AutoAllocateResult MapResult(Allocation allocation) =>
            new AutoAllocateResult {
                AllocationId = allocation.Id,
                Amount = allocation.Amount,
                AccountId = allocation.AccountId,
                WithdrawalGroupId = allocation.WithdrawalGroupId
            };
    }
}