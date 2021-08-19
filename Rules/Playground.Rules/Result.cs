namespace Playground.Rules {
    public class Result {
        public string Name { get; set; } = string.Empty;
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
        public object? Output { get; set; }
    }
}