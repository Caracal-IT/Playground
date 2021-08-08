using System.Collections.Generic;

namespace PaymentEngine.UseCases.Payments.Process {
    public class ProcessResponse : List<ExportResponse> {
        public ProcessResponse(IEnumerable<ExportResponse> responses) => 
            AddRange(responses);
    }
}