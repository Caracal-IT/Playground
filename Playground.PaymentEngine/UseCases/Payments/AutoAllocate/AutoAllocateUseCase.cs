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
            var withdrawals = _store.GetWithdrawals(withdrawalGroup.WithdrawalIds);
            var withdrawalAmount = withdrawals.Sum(w => w.Amount);

            if (withdrawalAmount <= 0M)
                return new List<AutoAllocateResult>();

            var customer = _store.GetCustomer(withdrawalGroup.CustomerId);
            var accounts = _store.GetCustomerAccounts(customer.Id).ToList();
            var allocationAmount = Math.Floor(withdrawalAmount / accounts.Count);
            var allocatedAmount = 0M;

            foreach (var account in accounts) {
                var amount = account.Equals(accounts.Last()) ? withdrawalAmount - allocatedAmount : allocationAmount;

                if (amount == 0)
                    break;
                
                var allocation = CreateAllocation(account, amount);
                allocation = _store.SaveAllocation(allocation);
                result.Add(MapResult(allocation));
                allocatedAmount += allocationAmount;
            }

            return result;

            Allocation CreateAllocation(Account account, decimal amount) =>
                new() {
                    AccountId = account.Id,
                    Amount = amount,
                    AllocationStatusId = 1,
                    WithdrawalGroupId = withdrawalGroupId
                };
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