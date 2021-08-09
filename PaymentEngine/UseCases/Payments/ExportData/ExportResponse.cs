using System.Collections.Generic;
using PaymentEngine.Model;

namespace PaymentEngine.UseCases.Payments.ExportData {
    public class ExportResponseData {
        public List<ExportAllocation> Allocations { get; set; } = new();
        public List<string> Data { get; set; }
    }
}