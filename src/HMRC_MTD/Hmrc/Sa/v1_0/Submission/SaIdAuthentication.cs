namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submission;

/// <summary>
/// Represents authentication credentials for SA submission, including UTR,
/// NINO, and agent or taxpayer identifiers.
/// </summary>
public class SaIdAuthentication
{
    public string SenderID { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
