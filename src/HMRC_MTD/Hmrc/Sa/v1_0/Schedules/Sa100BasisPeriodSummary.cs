namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Schedules;

/// <summary>
/// Summarises the basis period applied to self‑employment income within SA100,
/// including start/end dates and any transitional adjustments.
/// </summary>
public class Sa100BasisPeriodSummary
{
    public DateTime BasisPeriodStart { get; set; }
    public DateTime BasisPeriodEnd { get; set; }

    public decimal OverlapProfit { get; set; }
    public decimal OverlapReliefUsed { get; set; }

    public decimal TransitionalProfit { get; set; }
    public decimal TransitionalRelief { get; set; }
    public decimal TransitionalProfitSpread { get; set; }
}
