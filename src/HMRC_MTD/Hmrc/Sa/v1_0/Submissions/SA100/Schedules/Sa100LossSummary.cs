namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.SA100.Schedules;

/// <summary>
/// Summarises losses declared in SA100, including brought‑forward, used,
/// and carried‑forward amounts across relevant schedules.
/// </summary>
public class Sa100LossSummary
{
    public decimal LossBroughtForward { get; set; }
    public decimal LossUsedThisYear { get; set; }
    public decimal LossCarriedForward { get; set; }
    public decimal LossUsedAgainstOtherIncome { get; set; }
}
