using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawals;

using ViewModel = Playground.PaymentEngine.Models.Withdrawals;

using static Playground.PaymentEngine.Extensions.WebExtensions;

namespace Playground.PaymentEngine.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class WithdrawalController: ControllerBase {
        private readonly IMapper _mapper;
        
        public WithdrawalController(IMapper mapper) => 
            _mapper = mapper;

        public Task<IEnumerable<ViewModel.Withdrawal>> GetAsync([FromServices] GetWithdrawalsUseCase useCase, CancellationToken cancellationToken) =>
            ExecuteAsync(async () => {
                var result = await useCase.ExecuteAsync(new GetWithdrawalsRequest(), cancellationToken);
                return _mapper.Map<IEnumerable<ViewModel.Withdrawal>>(result.Withdrawals);
            });
    }
}