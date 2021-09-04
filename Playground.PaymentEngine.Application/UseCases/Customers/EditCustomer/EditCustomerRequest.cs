using System;

namespace Playground.PaymentEngine.Application.UseCases.Customers.EditCustomer {
    public record EditCustomerRequest {
        public long CustomerId { get; set; }
        public decimal? Balance { get; set; } = null;
        public IEnumerable<MetaDataUpdate> MetaData { get; set; } = Array.Empty<MetaDataUpdate>();
    }
}