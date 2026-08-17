namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.SA100.Submission;

/// <summary>
/// Represents the full Self Assessment submission envelope, including header,
/// sender details, authentication, and attached schedule documents.
/// </summary>
public class SaEnvelope
{
    public SaEnvelopeHeader Header { get; set; } = new();
    public string IRmark { get; set; } = string.Empty;
    public List<SaScheduleDocument> Body { get; set; } = new();

    public string ToXml() => SaEnvelopeSerializer.Serialize(this);

}
