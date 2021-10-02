namespace Playground.PaymentEngine.Api.Extensions;

public static class StringExtensions {
    public static async Task<string> ReadFromFileAsync(this string path, CancellationToken cancellationToken) {
        using var sr = new StreamReader(path);
        return await sr.ReadToEndAsync();
    }
}