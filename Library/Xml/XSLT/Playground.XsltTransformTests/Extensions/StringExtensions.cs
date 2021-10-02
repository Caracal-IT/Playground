namespace Playground.XsltTransformTests.Extensions;

public static class StringExtensions {
    public static async Task<string> ReadFromFileAsync(this string path) {
        using var sr = new StreamReader(path);
        return await sr.ReadToEndAsync();
    }
}