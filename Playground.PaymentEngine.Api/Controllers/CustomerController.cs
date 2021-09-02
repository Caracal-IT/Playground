namespace Playground.PaymentEngine.Api.Controllers {
    [ApiController]
    [Route("customers")]
    public class CustomerController: ControllerBase {
        private readonly IMapper _mapper;
        
        public CustomerController(IMapper mapper) => 
            _mapper = mapper;
    }
}