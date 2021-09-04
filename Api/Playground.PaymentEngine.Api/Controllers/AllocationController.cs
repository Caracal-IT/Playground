using Playground.PaymentEngine.Application.UseCases.Allocations.AutoAllocate;
using Playground.PaymentEngine.Application.UseCases.Allocations.CreateAllocation;
using Playground.PaymentEngine.Application.UseCases.Allocations.DeleteAllocation;
using Playground.PaymentEngine.Application.UseCases.Allocations.GetAllocation;
using Playground.PaymentEngine.Application.UseCases.Allocations.GetAllocations;
 
using ViewModel = Playground.PaymentEngine.Api.Models.Allocations;

namespace Playground.PaymentEngine.Api.Controllers {
    [ApiController]
    [Route("allocations")]
    public class AllocationController: ControllerBase {
        private readonly IMapper _mapper;
        
        public AllocationController(IMapper mapper) => 
            _mapper = mapper;

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<ViewModel.Allocation>>> GetAsync([FromServices] GetAllocationsUseCase useCase, CancellationToken cancellationToken) =>
            await ExecuteAsync(async () => {
                var response = await useCase.ExecuteAsync(cancellationToken);
                return Ok(_mapper.Map<IEnumerable<ViewModel.Allocation>>(response.Allocations));
            });

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ViewModel.Allocation>> GetAsync([FromServices] GetAllocationUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
            await ExecuteAsync<ActionResult<ViewModel.Allocation>>(async () => {
                var response = await useCase.ExecuteAsync(id, cancellationToken);
                
                if (response.Allocation == null)
                    return NotFound();
                
                return Ok(_mapper.Map<ViewModel.Allocation>(response.Allocation));
            });
        
        [HttpPost]
        public async Task<ActionResult<ViewModel.Allocation>> PostAsync([FromServices] CreateAllocationUseCase useCase, [FromBody] ViewModel.CreateAllocationRequest request, CancellationToken cancellationToken) =>
            await ExecuteAsync(async () => {
                var result = await useCase.ExecuteAsync(_mapper.Map<CreateAllocationRequest>(request), cancellationToken);
                return Ok(_mapper.Map<ViewModel.Allocation>(result.Allocation));
            });
        
        [HttpDelete("{id:long}")]
        public Task<ActionResult> DeleteAsync([FromServices] DeleteAllocationUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
            ExecuteAsync<ActionResult>(async () => {
                await useCase.ExecuteAsync(id, cancellationToken);
                return NoContent();
            });
        
        [HttpPost("auto-allocate")]
        public async Task<ActionResult<IEnumerable<ViewModel.AutoAllocateResult>>> AutoAllocateAsync([FromServices] AutoAllocateUseCase useCase, ViewModel.AutoAllocateRequest request, CancellationToken cancellationToken) =>
            await ExecuteAsync<ActionResult<IEnumerable<ViewModel.AutoAllocateResult>>>(async () => {
                var response = await useCase.ExecuteAsync(request.WithdrawalGroups, cancellationToken);
                return Ok(_mapper.Map<IEnumerable<ViewModel.AutoAllocateResult>>(response.AllocationResults));
            });
    }
}