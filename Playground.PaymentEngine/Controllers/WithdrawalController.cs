using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
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

        [EnableQuery]
        public Task<IEnumerable<ViewModel.Withdrawal>> GetAsync([FromServices] GetWithdrawalsUseCase useCase, CancellationToken cancellationToken) =>
            ExecuteAsync(async () => {
                var result = await useCase.ExecuteAsync(new GetWithdrawalsRequest(), cancellationToken);
                return _mapper.Map<IEnumerable<ViewModel.Withdrawal>>(result.Withdrawals);
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