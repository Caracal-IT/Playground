using Playground.PaymentEngine.Models.Allocations;
using Playground.PaymentEngine.UseCases.Allocations.AutoAllocate;

using ViewModel = Playground.PaymentEngine.Models.Allocations;

namespace Playground.PaymentEngine.Controllers {
    [ApiController]
    [Route("allocations")]
    public class AllocationController: ControllerBase {
        private readonly IMapper _mapper;
        
        public AllocationController(IMapper mapper) => 
            _mapper = mapper;

        [HttpPost("auto-allocate")]
        public async Task<ActionResult<IEnumerable<ViewModel.AutoAllocateResult>>> AutoAllocateAsync([FromServices] AutoAllocateUseCase useCase, AutoAllocateRequest request, CancellationToken cancellationToken) =>
            await ExecuteAsync<ActionResult<IEnumerable<ViewModel.AutoAllocateResult>>>(async () => {
                var response = await useCase.ExecuteAsync(request.WithdrawalGroups, cancellationToken);
                return Ok(_mapper.Map<IEnumerable<ViewModel.AutoAllocateResult>>(response.AllocationResults));
            });
    }
}