using System.Collections.Generic;

namespace Router {
    public class Terminal {
        public string Name { get; set; } = string.Empty;
        public int RetryCount { get; set; }
        public string? Xslt { get; set; }
        
        public List<Setting> Settings { get; set; } = new();
    }
}