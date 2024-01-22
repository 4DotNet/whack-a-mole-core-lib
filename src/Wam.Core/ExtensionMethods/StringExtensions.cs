using System.Security.Cryptography;
namespace Wam.Core.ExtensionMethods;

public static class StringExtensions
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static string GenerateGameCode(int length = 6)
    {
        var result = new string(
            Enumerable
            .Repeat(Chars, length)
            .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)])
            .ToArray());
        return result;
    }
}