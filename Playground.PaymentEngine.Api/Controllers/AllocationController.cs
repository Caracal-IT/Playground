using Playground.PaymentEngine.Api.Models.Allocations;
using Playground.PaymentEngine.Application.UseCases.Allocations.AutoAllocate;
using AutoAllocateResult = Playground.PaymentEngine.Api.Models.Allocations.AutoAllocateResult;
using ViewModel = Playground.PaymentEngine.Api.Models.Allocations;

namespace Playground.PaymentEngine.Api.Controllers {
    [ApiController]
    [Route("allocations")]
    public class AllocationController: ControllerBase {
        private readonly IMapper _mapper;
        
        public AllocationController(IMapper mapper) => 
            _mapper = mapper;

        [HttpPost("auto-allocate")]
        public async Task<ActionResult<IEnumerable<AutoAllocateResult>>> AutoAllocateAsync([FromServices] AutoAllocateUseCase useCase, AutoAllocateRequest request, CancellationToken cancellationToken) =>
            await ExecuteAsync<ActionResult<IEnumerable<AutoAllocateResult>>>(async () => {
                var response = await useCase.ExecuteAsync(request.WithdrawalGroups, cancellationToken);
                return Ok(_mapper.Map<IEnumerable<AutoAllocateResult>>(response.AllocationResults));
            });
    }
}