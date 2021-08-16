using System.Collections.Generic;

namespace Playground.Router.Old {
    public class OldTerminal {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int RetryCount { get; set; }
        public string? Xslt { get; set; }
        
        public List<Setting> Settings { get; set; } = new();
    }
}