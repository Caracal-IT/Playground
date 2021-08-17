using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Extensions.Caching.Memory;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Services.CacheService;

namespace Playground.PaymentEngine.Stores {
    public class FilePaymentStore: PaymentStore {
        private Store _store;
        private readonly ICacheService _cacheService;

        public FilePaymentStore(ICacheService cacheService) {
            _cacheService = cacheService;
            LoadStore();
        }

        public Store GetStore() => _store;

        public Account GetAccount(long id) =>
            _store.Accounts
                  .AccountList
                  .FirstOrDefault(a => a.Id == id) ?? new Account();
        
        public Allocation GetAllocation(long id) =>
            _store.Allocations
                  .AllocationList
                  .FirstOrDefault(a => a.Id == id)
                  ??new Allocation();

        public ExportAllocation GetExportAllocation(long allocationId) {
            var allocation = GetAllocation(allocationId);
            var account = GetAccount(allocation.AccountId);

            return new ExportAllocation {
                AllocationId = allocation.Id,
                Amount = allocation.Amount + allocation.Charge,
                AccountId = account.Id,
                AccountTypeId = account.AccountTypeId,
                CustomerId = account.CustomerId,
                MetaData = account.MetaData
            };
        }

        public IEnumerable<ExportAllocation> GetExportAllocations(IEnumerable<long> allocationIds) =>
            allocationIds.Select(GetExportAllocation)
                         .Where(a => a.AccountId > 0);

        public IEnumerable<Allocation> GetAllocationsByReference(string reference) =>
            _store.Allocations
                .AllocationList
                .Where(a => !string.IsNullOrWhiteSpace(a.Terminal) && a.Reference.Equals(reference));

        public void SetAllocationStatus(long id, long statusId, string terminal = null, string reference = null) {
            var allocation = GetAllocation(id);
            allocation.AllocationStatusId = statusId;
            allocation.Terminal = terminal;
            allocation.Reference = reference;
        }

        public IEnumerable<Terminal> GetActiveAccountTypeTerminals(long accountTypeId) =>
            _store.TerminalMaps
                .TerminalMapList
                .Join(_store.Terminals.TerminalList,
                    tm => tm.TerminalId,
                    t => t.Id,
                    (tm, t) => new { Map = tm, Terminal = t }
                )
                .Where(t => t.Map.Enabled && t.Map.AccountTypeId == accountTypeId)
                .OrderBy(t => t.Map.Order)
                .Select(t => t.Terminal)
                .ToList();

        public IEnumerable<Terminal> GetTerminals() =>
            _cacheService.GetValue(nameof(GetTerminals), () => _store.Terminals.TerminalList);

        public void LogTerminalResults(IEnumerable<TerminalResult> results) =>
            _store.TerminalResults.TerminalResultList.AddRange(results);
        
        private void LoadStore() {
            var path = Path.Join("Resources", "Data", "store.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            _store = (Store) new XmlSerializer(typeof(Store)).Deserialize(fileStream);
        }
    }
}