namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Schedules;

/// <summary>
/// Summarises capital allowance totals included in SA100, aggregated from
/// self‑employment and property schedules.
/// </summary>
public class Sa100CapitalAllowanceSummary
{
    public decimal CapitalAllowancesTotal { get; set; }
    public decimal AnnualInvestmentAllowance { get; set; }
    public decimal WritingDownAllowance { get; set; }
    public decimal BalancingCharges { get; set; }
    public decimal BalancingAllowances { get; set; }
}
