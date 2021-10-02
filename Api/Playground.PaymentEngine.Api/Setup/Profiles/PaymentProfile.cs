namespace Playground.PaymentEngine.Api.Setup.Profiles;

using Process = Playground.PaymentEngine.Application.UseCases.Payments.Process;
using ViewModel = Playground.PaymentEngine.Api.Models.Payments;

public class PaymentProfile : Profile {
    public PaymentProfile() {
        CreateMap<ViewModel.ProcessRequest, Process.ProcessRequest>();
        CreateMap<Process.ExportResponse, ViewModel.ExportResponse>();
    }
}