using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawals;

namespace Playground.PaymentEngine.Setup.Application {
    public static class WithdrawalSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<GetWithdrawalsUseCase>();
        }

       // public static void Register(WebApplication app) {
            //app.MapGet("/withdrawals",
              //  ([FromServices] GetWithdrawalsUseCase useCase, CancellationToken cancellationToken) =>
                //    ExecuteAsync(() => useCase.ExecuteAsync(new GetWithdrawalsRequest(), cancellationToken)));
       // }
    }
}