using System;
using System.Collections.Generic;

namespace PaymentEngine.UseCases.Payments.ExportData {
    public class ExportDataRequest {
        public bool Consolidate { get; set; } = false;
        public List<string> PreferredTerminals { get; set; } = new();
        public string Template { get; set; } = string.Empty;
        
        // ReSharper disable once CollectionNeverUpdated.Global
        public List<long> Allocations { get; set; } = new();
    }
}