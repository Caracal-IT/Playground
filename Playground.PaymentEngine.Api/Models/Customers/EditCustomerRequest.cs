using System;

namespace Playground.PaymentEngine.Api.Models.Customers {
    public class EditCustomerRequest {
        public decimal? Balance { get; set; } = null;
        public IEnumerable<MetaDataUpdate> MetaData { get; set; } = Array.Empty<MetaDataUpdate>();
    }
}