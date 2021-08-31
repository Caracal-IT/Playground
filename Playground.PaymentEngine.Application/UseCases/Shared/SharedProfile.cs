using Data = Playground.PaymentEngine.Store.Model;

namespace Playground.PaymentEngine.Application.UseCases.Shared {
    public class SharedProfile: Profile {
        public SharedProfile() {
            CreateMap<Data.MetaData, MetaData>().ReverseMap();
            CreateMap<Data.Setting, Setting>().ReverseMap();
        }
    }
}