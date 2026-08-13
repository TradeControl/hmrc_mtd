namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submission;

/// <summary>
/// Contains message‑level metadata for the SA submission, including timestamps,
/// correlation IDs, and sender references.
/// </summary>
public class SaMessageDetails
{
    public string Class { get; set; } = "SA";
    public string Qualifier { get; set; } = "request";
    public string Function { get; set; } = "submit";
    public string TransactionID { get; set; } = Guid.NewGuid().ToString();
}
