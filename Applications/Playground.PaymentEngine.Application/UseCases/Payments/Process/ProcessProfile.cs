namespace Playground.PaymentEngine.Application.UseCases.Payments.Process;

using Store.Terminals.Model;
using Data = Playground.PaymentEngine.Store.Withdrawals.Model;

public class ProcessProfile : Profile {
    public ProcessProfile() {
        CreateMap<ExportResponse, TerminalResult>()
            .ForMember(r => r.Date, opt => opt.MapFrom(s => DateTime.UtcNow))
            .ForMember(r => r.Success, opt => opt.MapFrom(s => s.Code != null && s.Code.Equals("00")));
    }
}