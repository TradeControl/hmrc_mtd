using System.Security.Cryptography;
using System.Text;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submission;

/// <summary>
/// Generates the IRmark checksum for the canonicalized SA submission XML,
/// ensuring integrity and HMRC acceptance.
/// </summary>
public static class SaIrmarkGenerator
{
    /// <summary>
    /// Computes the IRmark:
    /// Base64(SHA1(canonicalised XML bytes))
    /// </summary>
    public static string GenerateIrmark(string canonicalXml)
    {
        if (string.IsNullOrEmpty(canonicalXml))
            return string.Empty;

        var bytes = Encoding.UTF8.GetBytes(canonicalXml);

        using var sha1 = SHA1.Create();
        var hash = sha1.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }
}
