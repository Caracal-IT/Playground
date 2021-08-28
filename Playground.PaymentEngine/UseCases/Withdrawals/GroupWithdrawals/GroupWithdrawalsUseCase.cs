using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Playground.PaymentEngine.Stores.Withdrawals;

namespace Playground.PaymentEngine.UseCases.Withdrawals.GroupWithdrawals {
    public class GroupWithdrawalsUseCase {
        private readonly WithdrawalStore _store;  
        private readonly IMapper _mapper;
        
        public GroupWithdrawalsUseCase(WithdrawalStore store, IMapper mapper) {
            _store = store;
            _mapper = mapper;
        }

        public async Task<GroupWithdrawalsResponse> ExecuteAsync(IEnumerable<long> withdrawals, CancellationToken cancellationToken) {
            var groups = await _store.GroupWithdrawalsAsync(withdrawals, cancellationToken);
            return new GroupWithdrawalsResponse {
                WithdrawalGroups = _mapper.Map<IEnumerable<WithdrawalGroup>>(groups)
            };
        }
    }
}