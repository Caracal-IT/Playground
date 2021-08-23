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
        private readonly WithdrawalStore _withdrawalStore;
        private readonly CustomerStore _customerStore;
        private readonly AllocationStore _allocationStore;

        public AutoAllocateUseCase(AccountStore accountStore, WithdrawalStore withdrawalStore, AllocationStore allocationStore, CustomerStore customerStore) {
            _accountStore = accountStore;
            _withdrawalStore = withdrawalStore;
            _customerStore = customerStore;
            _allocationStore = allocationStore;
        }

        public async Task<AutoAllocateResponse> ExecuteAsync(AutoAllocateRequest request, CancellationToken cancellationToken) {
            await request.WithdrawalGroups.Select(RemoveAllocationsAsync).WhenAll(50);
            
            var results = await request.WithdrawalGroups.Select(w => AllocateFundsAsync(w, cancellationToken)).WhenAll(50);
            return new AutoAllocateResponse { AllocationResults = results.SelectMany(a => a).ToList() };

            async Task RemoveAllocationsAsync(long withdrawalGroupId) =>
                await _allocationStore.RemoveAllocationsAsync(withdrawalGroupId, cancellationToken);
        }

        private async Task<List<AutoAllocateResult>> AllocateFundsAsync(long withdrawalGroupId, CancellationToken cancellationToken) {
            var result = new List<AutoAllocateResult>();
            
            var withdrawalGroup = await _withdrawalStore.GetWithdrawalGroupAsync(withdrawalGroupId, cancellationToken);
            var withdrawals = await _withdrawalStore.GetWithdrawalGroupWithdrawalsAsync(withdrawalGroupId, cancellationToken);
            var withdrawalAmount = withdrawals.Sum(w => w.Amount);

            if (withdrawalAmount <= 0M)
                return new List<AutoAllocateResult>();

            var customer = await _customerStore.GetCustomerAsync(withdrawalGroup.CustomerId, cancellationToken);
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
                await AddNewAllocationAsync(account, amount);
                withdrawalAmount -= amount;
                
                if(withdrawalAmount <= 0)
                    break;
            }

            if (withdrawalAmount <= 0) return result;
            
            var preferredAcc = orderedAccounts.FirstOrDefault(a => a.IsPreferredAccount) ?? orderedAccounts.First();
            await AddNewAllocationAsync(preferredAcc, withdrawalAmount);
            
            return result;
            
            async Task AddNewAllocationAsync(Account account, decimal amount) {
                 var allocation  = await _allocationStore.SaveAllocationAsync(new Allocation {
                    AccountId = account.Id,
                    Amount = amount,
                    AllocationStatusId = 1,
                    WithdrawalGroupId = withdrawalGroupId
                 }, cancellationToken);
                 
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