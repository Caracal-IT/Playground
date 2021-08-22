using System;
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
            var allocations = _store.GetStore().Allocations.AllocationList;
            
            _store.GetStore()
                .Allocations
                .AllocationList
                .Where(a => a.WithdrawalGroupId == withdrawalGroupId)
                .ToList()
                .ForEach(a => allocations.Remove(a));
        }

        private List<AutoAllocateResult> AllocateFunds(long withdrawalGroupId) {
            var result = new List<AutoAllocateResult>();
            
            
            var store = _store.GetStore();
            var withdrawalGroup = store.WithdrawalGroups
                .WithdrawalGroupList
                .First(g => g.Id == withdrawalGroupId);
            
            var withdrawals = _store.GetWithdrawals(withdrawalGroup.WithdrawalIds);
            var withdrawalAmount = withdrawals.Sum(w => w.Amount);

            if (withdrawalAmount <= 0M)
                return new List<AutoAllocateResult>();
            
            var withdrawalId = withdrawalGroup.WithdrawalIds.First();
            
            var customer = store
                .Customers
                .CustomerList
                .Join(
                    store.Withdrawals.WithdrawalList,
                    c => c.Id,
                    w => w.CustomerId,
                    (c, w) => new { Customer = c, Withdrawal = w }
                )
                .Where(c => c.Withdrawal.Id == withdrawalId)
                .Select(c => c.Customer)
                .First();

            var accounts = store.Accounts.AccountList.Where(a => a.CustomerId == customer.Id).ToList();
            var startId = store.Allocations.AllocationList.Any() ? store.Allocations.AllocationList.Last().Id + 1 : 1;

            var allocationAmount = Math.Floor(withdrawalAmount / accounts.Count);
            
            var allocatedAmount = 0M;

            for (var i = 0; i < accounts.Count; i++) {
                var account = accounts[i];

                var allocation = new Allocation {
                    Id = startId + i, 
                    AccountId = account.Id,
                    AllocationStatusId = 1,
                    WithdrawalGroupId = withdrawalGroupId
                };

                if (i == accounts.Count - 1)
                    allocation.Amount = allocatedAmount;
                else
                    allocation.Amount = withdrawalAmount - allocatedAmount;
                
                store.Allocations.AllocationList.Add(allocation);

                result.Add(new AutoAllocateResult {
                    AllocationId = allocation.Id,
                    Amount = allocation.Amount,
                    AccountId = account.Id,
                    WithdrawalGroupId = withdrawalGroupId
                });
                
                allocatedAmount += allocationAmount;
            }

            return result;
        }
    }
}