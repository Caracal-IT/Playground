namespace Router {
    public class Terminal {
        public string Name { get; set; } = string.Empty;
        public int RetryCount { get; set; }
        public string? OutXslt { get; set; }
        public string? InXslt { get; set; }
    }
}