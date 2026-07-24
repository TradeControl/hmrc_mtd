namespace TradeControl.Tax.UK.Models.Tc;

public sealed class TcBusinessTaxView
{
    public string TaxSourceCode { get; init; } = string.Empty;

    public string TagCode { get; init; } = string.Empty;

    public DateTime PeriodFrom { get; init; }

    public DateTime PeriodTo { get; init; }

    public decimal TaxableAmount { get; init; }
}
