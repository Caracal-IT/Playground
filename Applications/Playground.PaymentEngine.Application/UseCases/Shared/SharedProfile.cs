namespace Playground.PaymentEngine.Application.UseCases.Shared;

using Data = Playground.PaymentEngine.Store.Model;

public class SharedProfile : Profile {
    public SharedProfile() {
        CreateMap<Data.MetaData, MetaData>().ReverseMap();
        CreateMap<Data.Setting, Setting>().ReverseMap();
    }
}