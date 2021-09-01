using Playground.PaymentEngine.Application.UseCases.Withdrawals.ChangeWithdrawalStatus;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.CreateWithdrawal;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.DeleteWithdrawal;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawal;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawals;
using ViewModel = Playground.PaymentEngine.Models.Withdrawals;

namespace Playground.PaymentEngine.Controllers {
    [ApiController]
    [Route("withdrawals")]
    public class WithdrawalController: ControllerBase {
        private readonly IMapper _mapper;
        
        public WithdrawalController(IMapper mapper) => 
            _mapper = mapper;
        
        [HttpPost]
        public async Task<ActionResult<ViewModel.Withdrawal>> Post([FromServices] CreateWithdrawalUseCase useCase, [FromBody] ViewModel.CreateWithdrawalRequest request, CancellationToken cancellationToken) =>
            await ExecuteAsync(async () => {
                var result = await useCase.ExecuteAsync(_mapper.Map<CreateWithdrawalRequest>(request), cancellationToken);
                return Ok(_mapper.Map<ViewModel.Withdrawal>(result.Withdrawal));
            });

        [EnableQuery]
        public async Task<ActionResult<IEnumerable<ViewModel.Withdrawal>>> GetAsync([FromServices] GetWithdrawalsUseCase useCase, CancellationToken cancellationToken) =>
            await ExecuteAsync(async () => {
                var result = await useCase.ExecuteAsync(new GetWithdrawalsRequest(), cancellationToken);
                return Ok(_mapper.Map<IEnumerable<ViewModel.Withdrawal>>(result.Withdrawals));
            });

        [HttpGet("{id:long}")]
        public Task<ActionResult<ViewModel.Withdrawal>> GetAsync([FromServices] GetWithdrawalUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
            ExecuteAsync<ActionResult<ViewModel.Withdrawal>>(async () => {
                var withdrawal = await useCase.ExecuteAsync(id, cancellationToken);

                if (withdrawal?.Withdrawal == null)
                    return NotFound();
                
                return Ok(_mapper.Map<ViewModel.Withdrawal>(withdrawal.Withdrawal));
            });
        
        [HttpDelete("{id:long}")]
        public Task<ActionResult> DeleteAsync([FromServices] DeleteWithdrawalUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
            ExecuteAsync<ActionResult>(async () => {
                await useCase.ExecuteAsync(id, cancellationToken);
                return NoContent();
            });
        
        [HttpPatch("{id:long}")]
        public Task<ActionResult> UpdateStatusAsync([FromServices] ChangeWithdrawalStatusUseCase useCase, [FromRoute] long id, [FromBody] ViewModel.Status status, CancellationToken cancellationToken) =>
            ExecuteAsync<ActionResult>(async () => {
                await useCase.ExecuteAsync(id, status.StatusId, cancellationToken);
                return NoContent();
            });
        
        
    }
}

/*
 [Queryable(AllowedQueryOptions=
    AllowedQueryOptions.Skip | AllowedQueryOptions.Top)]
 public PageResult<Product> Get(ODataQueryOptions<Product> options)
{
    ODataQuerySettings settings = new ODataQuerySettings()
    {
        PageSize = 5
    };

    IQueryable results = options.ApplyTo(_products.AsQueryable(), settings);

    return new PageResult<Product>(
        results as IEnumerable<Product>, 
        Request.GetNextPageLink(), 
        Request.GetInlineCount());
}
*/