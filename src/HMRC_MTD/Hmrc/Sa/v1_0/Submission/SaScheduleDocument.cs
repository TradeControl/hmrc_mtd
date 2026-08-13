namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submission;

/// <summary>
/// Represents a single schedule document (e.g., SA100, SA102, SA108) embedded
/// within the SA submission envelope.
/// </summary>
public class SaScheduleDocument
{
    public string Name { get; set; } = string.Empty;   // e.g. "SA100"
    public string XmlContent { get; set; } = string.Empty;
}
