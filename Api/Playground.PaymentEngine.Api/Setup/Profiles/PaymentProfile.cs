using Playground.PaymentEngine.Api.Models.Payments;
using Process = Playground.PaymentEngine.Application.UseCases.Payments.Process;
using ViewModel = Playground.PaymentEngine.Api.Models.Payments;

namespace Playground.PaymentEngine.Api.Setup.Profiles {
    public class PaymentProfile: Profile {
        public PaymentProfile() {
            CreateMap<ProcessRequest, Process.ProcessRequest>();
            CreateMap<Process.ExportResponse, ExportResponse>();
        }
    }
}