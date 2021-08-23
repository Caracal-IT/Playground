using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Helpers;
using Playground.PaymentEngine.Stores.Accounts;
using Playground.PaymentEngine.Stores.Accounts.Model;
using Playground.PaymentEngine.Stores.Allocations;
using Playground.PaymentEngine.Stores.Allocations.Model;
using Playground.PaymentEngine.Stores.Customers;
using Playground.PaymentEngine.Stores.Withdrawals;

namespace Playground.PaymentEngine.UseCases.Payments.AutoAllocate {
    public class AutoAllocateUseCase {
        private readonly AccountStore _accountStore;
        private readonly WithdrawalStore _paymentStore;
        private readonly CustomerStore _customerStore;
        private readonly AllocationStore _allocationStore;

        public AutoAllocateUseCase(AccountStore accountStore, WithdrawalStore paymentStore, AllocationStore allocationStore, CustomerStore customerStore) {
            _accountStore = accountStore;
            _paymentStore = paymentStore;
            _customerStore = customerStore;
            _allocationStore = allocationStore;
        }

        public async Task<AutoAllocateResponse> ExecuteAsync(AutoAllocateRequest request, CancellationToken cancellationToken) {
            request.WithdrawalGroups.ForEach(RemoveAllocations);
            var results = await request.WithdrawalGroups.Select(w => AllocateFundsAsync(w, cancellationToken)).WhenAll(50);
            return new AutoAllocateResponse{AllocationResults = results.SelectMany(a => a).ToList()};
        }

        private void RemoveAllocations(long withdrawalGroupId) => 
            _allocationStore.RemoveAllocations(withdrawalGroupId);

        private async Task<List<AutoAllocateResult>> AllocateFundsAsync(long withdrawalGroupId, CancellationToken cancellationToken) {
            var result = new List<AutoAllocateResult>();
            
            var withdrawalGroup = _paymentStore.GetWithdrawalGroup(withdrawalGroupId);
            var withdrawals = _paymentStore.GetWithdrawalGroupWithdrawals(withdrawalGroupId);
            var withdrawalAmount = withdrawals.Sum(w => w.Amount);

            if (withdrawalAmount <= 0M)
                return new List<AutoAllocateResult>();

            var customer = _customerStore.GetCustomer(withdrawalGroup.CustomerId);
            var accountEnum = await _accountStore.GetCustomerAccountsAsync(customer.Id, cancellationToken);
            var accounts = accountEnum.ToList();
            var accountTypes = await _accountStore.GetAccountTypesAsync(accounts.Select(a => a.AccountTypeId), cancellationToken);

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
                 var allocation  = _allocationStore.SaveAllocation(new Allocation {
                    AccountId = account.Id,
                    Amount = amount,
                    AllocationStatusId = 1,
                    WithdrawalGroupId = withdrawalGroupId
                 });
                 
                 result.Add(MapResult(allocation));
            }
        }

        private static AutoAllocateResult MapResult(Allocation allocation) =>
            new AutoAllocateResult {
                AllocationId = allocation.Id,
                Amount = allocation.Amount,
                AccountId = allocation.AccountId,
                WithdrawalGroupId = allocation.WithdrawalGroupId
            };
    }
}