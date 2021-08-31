using UseCase = Playground.PaymentEngine.Application.UseCases.Shared;

namespace Playground.PaymentEngine.Models.Shared {
    public class SharedProfile: Profile {
        public SharedProfile() {
            CreateMap<UseCase.MetaData, MetaData>().ReverseMap();
            CreateMap<UseCase.Setting, Setting>().ReverseMap();
        }
    }
}