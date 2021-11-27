namespace Playground.PaymentEngine.Application.UseCases.Payments.Process;

using Core.Extensions;
using Playground.PaymentEngine.Store.Allocations.Model;
using Store.Terminals.Model;
using Router.Services;
using Xml;

using static Core.Hashing;
using static Xml.Serialization.Serializer;

public class ProcessUseCase {
    private readonly TerminalStore _terminalStore;
    private readonly IRoutingService _routingService;
    private readonly AllocationStore _allocationStore;
    private readonly AccountStore _accountStore;
    private readonly IMapper _mapper;

    public ProcessUseCase(AllocationStore allocationStore, AccountStore accountStore, TerminalStore terminalStore, IRoutingService routingService, IMapper mapper) 
    {
        _accountStore = accountStore;
        _allocationStore = allocationStore;
        _terminalStore = terminalStore;
        _routingService = routingService;
        _mapper = mapper;
    }

    public async Task<ProcessResponse> ExecuteAsync(ProcessRequest request, CancellationToken cancellationToken) {
        var transactionId = Guid.NewGuid();
        var allocations = await GetExportAllocationsAsync(request.Allocations, cancellationToken);
        var items = (request.Consolidate ? GetConsolidated() : GetExportData()).ToList();

        await items.Select(ExportAsync).WhenAll(maxConcurrentRequests: 50);

        await UpdateStatusesAsync();
        await SaveResultsAsync();

        return new ProcessResponse(items.Select(CreateResponse));

        ExportResponse CreateResponse(ExportData data) {
            if (data.Response.Any())
                return data.Response.Last();

            return new ExportResponse {
                Reference = data.Reference,
                Name = $"Account Type : {data.AccountTypeId}",
                Message = "Failed",
                Code = "-1"
            };
        }

        async Task ExportAsync(ExportData data) {
            var terminalEnum = await _terminalStore.GetActiveAccountTypeTerminalsAsync(data.AccountTypeId, cancellationToken);
            var terminals = terminalEnum.Select(t => t.Name);

            var req = new RoutingRequest(transactionId, nameof(ProcessUseCase), data.ToXml(), terminals!);
            var response = await _routingService.SendAsync(req, cancellationToken);

            var result = response.Select(r => r.Result).Select(DeSerialize<ExportResponse>);
            data.Response.AddRange(result);
        }

        async Task SaveResultsAsync() {
            var results = items.SelectMany(i => i.Response).Select(_mapper.Map<TerminalResult>);
            await _terminalStore.LogTerminalResultsAsync(results, cancellationToken);
        }

        async Task UpdateStatusesAsync() {
            await items.Select(SetStatusAsync).WhenAll(maxConcurrentRequests: 50);

            async Task SetStatusAsync(ExportData data) {
                var response = data.Response.FirstOrDefault(i => i.Code == "00") ?? new ExportResponse();
                var statusId = response.Code == "00" ? 4 : 5;
                var terminal = response.Terminal;

                await data.Allocations.Select(SetAllocationStatusAsync).WhenAll(maxConcurrentRequests: 50);

                async Task SetAllocationStatusAsync(ExportAllocation ea) =>
                    await _allocationStore.SetAllocationStatusAsync(ea.AllocationId, statusId, terminal!, data.Reference, cancellationToken);
            }
        }

        IEnumerable<ExportData> GetConsolidated() =>
            allocations
                .GroupBy(a => new { customerId = a.CustomerId, accountId = a.AccountId })
                .Select(a => new ExportData {
                    Allocations = a.Select(i => i).ToList(),
                    Amount = a.Sum(i => i.Amount),
                    AccountTypeId = a.First().AccountTypeId,
                    Reference = hash5(string.Join('*', a.Select(i => i.AllocationId))),
                    MetaData = a.First().MetaData
                });

        IEnumerable<ExportData> GetExportData() =>
            allocations.Select(a => new ExportData {
                Allocations = new List<ExportAllocation> { a },
                Amount = a.Amount,
                AccountTypeId = a.AccountTypeId,
                Reference = hash5($"{a.AllocationId}"),
                MetaData = a.MetaData
            });
    }
    
    private async Task<IEnumerable<ExportAllocation>> GetExportAllocationsAsync(IEnumerable<long> allocationIds, CancellationToken cancellationToken) {
        var accounts = _accountStore.GetAccounts();
        var exportAllocations = await allocationIds.Select(GetExportAllocationAsync).WhenAll(maxConcurrentRequests: 50);
        return exportAllocations.Where(a => a.AccountId > 0);

        async Task<ExportAllocation> GetExportAllocationAsync(long allocationId) {
            var allocations = await _allocationStore.GetAllocationsAsync(new[] { allocationId }, cancellationToken);
            var allocation = allocations.FirstOrDefault() ?? new Allocation();

            var account = accounts.First(a => a.Id == allocation.AccountId);
            
            return new ExportAllocation
            {
                AllocationId = allocation.Id,
                Amount = allocation.Amount + allocation.Charge,
                AccountId = account.Id,
                AccountTypeId = account.AccountTypeId,
                CustomerId = account.CustomerId,
                MetaData = _mapper.Map<List<MetaData>>(account.MetaData)
            };
        }
    }
}