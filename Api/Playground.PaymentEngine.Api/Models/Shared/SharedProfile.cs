namespace Playground.PaymentEngine.Api.Models.Shared;

using UseCase = Playground.PaymentEngine.Application.UseCases.Shared;

public class SharedProfile: Profile {
    public SharedProfile() {
        CreateMap<UseCase.MetaData, MetaData>().ReverseMap();
        CreateMap<UseCase.Setting, Setting>().ReverseMap();
    }
}