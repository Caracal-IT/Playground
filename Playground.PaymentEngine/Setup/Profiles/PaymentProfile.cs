using Process = Playground.PaymentEngine.UseCases.Payments.Process;
using ViewModel = Playground.PaymentEngine.Models.Payments;

namespace Playground.PaymentEngine.Setup.Profiles {
    public class PaymentProfile: Profile {
        public PaymentProfile() {
            CreateMap<ViewModel.ProcessRequest, Process.ProcessRequest>();
            CreateMap<Process.ExportResponse, ViewModel.ExportResponse>();
        }
    }
}