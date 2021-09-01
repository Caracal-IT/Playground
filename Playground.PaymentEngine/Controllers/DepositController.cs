using System;
using Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposits;
using ViewModel = Playground.PaymentEngine.Models.Deposits;

namespace Playground.PaymentEngine.Controllers {
    [ApiController]
    [Route("deposits")]
    public class DepositController: ControllerBase {
        private readonly IMapper _mapper;
        
        public DepositController(IMapper mapper) => 
            _mapper = mapper;

        [EnableQuery]
        public async Task<ActionResult<IEnumerable<ViewModel.Deposit>>> GetAsync([FromServices] GetDepositsUseCase useCase, CancellationToken cancellationToken) =>
            await ExecuteAsync(async () => {
                var response = await useCase.ExecuteAsync(cancellationToken);
                return Ok(_mapper.Map<IEnumerable<ViewModel.Deposit>>(response.Deposits));
            });
    }
}