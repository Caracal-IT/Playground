namespace Playground.Core;

public static class Hashing {
    public static string Hash256(string strToHash) {
        using var sha256 = SHA256.Create();
        return sha256.ComputeHash(Encoding.UTF8.GetBytes(strToHash))
            .Aggregate(string.Empty, (current, theByte) => current + theByte.ToString("x2"));
    }
    
    public static string hash5(string strToHash) {
        using var sha256 = MD5.Create();
        return sha256.ComputeHash(Encoding.UTF8.GetBytes(strToHash))
            .Aggregate(string.Empty, (current, theByte) => current + theByte.ToString("x2"));
    }
}