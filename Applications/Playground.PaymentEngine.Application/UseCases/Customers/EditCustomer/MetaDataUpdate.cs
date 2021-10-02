namespace Playground.PaymentEngine.Application.UseCases.Customers.EditCustomer;

public record MetaDataUpdate : MetaData {
    public bool Remove { get; set; }
}