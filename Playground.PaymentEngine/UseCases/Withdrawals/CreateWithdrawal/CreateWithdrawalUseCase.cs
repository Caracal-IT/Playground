using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Playground.PaymentEngine.Stores.Withdrawals;

using Data = Playground.PaymentEngine.Stores.Withdrawals.Model;

namespace Playground.PaymentEngine.UseCases.Withdrawals.CreateWithdrawal {
    public class CreateWithdrawalUseCase {
        private readonly WithdrawalStore _store;
        private readonly IMapper _mapper;
        
        public CreateWithdrawalUseCase(WithdrawalStore store, IMapper mapper) {
            _store = store;
            _mapper = mapper;
        }
        
        public async Task<CreateWithdrawalResponse> ExecuteAsync(CreateWithdrawalRequest request, CancellationToken cancellationToken) {
            var withdrawal = _mapper.Map<Data.Withdrawal>(request);
            withdrawal.WithdrawalStatusId = 0;
            withdrawal.DateRequested = DateTime.Now;

            var response = await  _store.CreateWithdrawalAsync(withdrawal, cancellationToken);
            
            return new CreateWithdrawalResponse{ Withdrawal = _mapper.Map<Withdrawal>(response) };
        }
    }
}