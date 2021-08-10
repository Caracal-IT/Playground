using System.Collections.Generic;

namespace Router {
    public class Request {
        public int RequestType { get; set; } 
        public string Data { get; set; } = string.Empty;
        public Dictionary<string, int> Terminals { get; set; } = new Dictionary<string, int>();
    }
}