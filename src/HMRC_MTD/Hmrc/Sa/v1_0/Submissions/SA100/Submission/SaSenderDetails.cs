namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.SA100.Submission;

/// <summary>
/// Contains details of the entity submitting the SA return, including software
/// vendor information and sender identifiers.
/// </summary>
public class SaSenderDetails
{
    public SaIdAuthentication IDAuthentication { get; set; } = new();
}
