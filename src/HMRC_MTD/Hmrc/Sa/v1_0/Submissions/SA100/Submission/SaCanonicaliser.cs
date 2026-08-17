using System.Text;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.SA100.Submission;

public static class SaCanonicaliser
{
    /// <summary>
    /// Produces a canonicalised XML string suitable for IRmark hashing.
    /// This is the minimal canonicalisation required by HMRC:
    /// - UTF-8
    /// - No BOM
    /// - Normalised line endings
    /// - Trimmed leading/trailing whitespace
    /// - No indentation (already enforced by serializer)
    /// </summary>
    public static string CanonicaliseEnvelopeXml(string xml)
    {
        if (string.IsNullOrWhiteSpace(xml))
            return string.Empty;

        // 1. Trim leading/trailing whitespace
        var trimmed = xml.Trim();

        // 2. Normalise line endings to '\n'
        var normalised = trimmed
            .Replace("\r\n", "\n")
            .Replace("\r", "\n");

        // 3. Ensure UTF-8 without BOM
        var utf8Bytes = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false)
            .GetBytes(normalised);

        return Encoding.UTF8.GetString(utf8Bytes);
    }
}
