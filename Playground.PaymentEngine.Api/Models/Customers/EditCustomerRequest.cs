using System;

namespace Playground.PaymentEngine.Api.Models.Customers {
    public record EditCustomerRequest {
        public decimal? Balance { get; set; } = null;
        public IEnumerable<MetaDataUpdate> MetaData { get; set; } = Array.Empty<MetaDataUpdate>();
    }
}