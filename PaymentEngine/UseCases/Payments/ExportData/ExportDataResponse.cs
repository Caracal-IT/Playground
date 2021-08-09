using System.Collections.Generic;

namespace PaymentEngine.UseCases.Payments.ExportData {
    public class ExportDataResponse : List<ExportResponseData> {
        public ExportDataResponse(IEnumerable<ExportResponseData> responses) => 
            AddRange(responses);
    }
}