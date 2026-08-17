namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.SA100.Submission;

public static class SaEnvelopeBuilder
{
    /// <summary>
    /// Builds a fully-formed SA submission envelope:
    /// 1. Serialises envelope without IRmark
    /// 2. Canonicalises XML
    /// 3. Generates IRmark
    /// 4. Inserts IRmark
    /// 5. Serialises final envelope
    /// </summary>
    public static string Build(SaEnvelope envelope)
    {
        if (envelope == null)
            throw new ArgumentNullException(nameof(envelope));

        // Step 1: Serialize envelope WITHOUT IRmark
        envelope.IRmark = string.Empty;
        var xmlWithoutIrmark = envelope.ToXml();

        // Step 2: Canonicalise
        var canonicalXml = SaCanonicaliser.CanonicaliseEnvelopeXml(xmlWithoutIrmark);

        // Step 3: Generate IRmark
        var irmark = SaIrmarkGenerator.GenerateIrmark(canonicalXml);

        // Step 4: Insert IRmark into envelope
        envelope.IRmark = irmark;

        // Step 5: Serialize final envelope WITH IRmark
        var finalXml = envelope.ToXml();

        return finalXml;
    }
}
