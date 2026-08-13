namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submission;

/// <summary>
/// Contains metadata for the SA submission envelope, including tax year,
/// submission type, and message identifiers.
/// </summary>
public class SaEnvelopeHeader
{
    public SaMessageDetails MessageDetails { get; set; } = new();
    public SaSenderDetails SenderDetails { get; set; } = new();
}
