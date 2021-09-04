using System;
using System.Collections.Generic;

namespace Playground.Router {
    public class Terminal {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int RetryCount { get; set; } = 2;
        public string Xslt { get; set; } = string.Empty;
        
        public IEnumerable<Setting> Settings { get; set; } = Array.Empty<Setting>();
    }
}