using Playground.PaymentEngine.Api.Models.Shared;

namespace Playground.PaymentEngine.Api.Models.Customers {
    public record MetaDataUpdate : MetaData {
        public bool Remove { get; set; }
    }
}