namespace TradeControl.Tax.UK.Models.Tc;

public sealed class TcVatStatement
{
    public int YearNumber { get; init; }

    public string Description { get; init; } = string.Empty;

    public string Period { get; init; } = string.Empty;

    public DateTime StartOn { get; init; }

    public decimal HomeSales { get; init; }

    public decimal HomePurchases { get; init; }

    public decimal ExportSales { get; init; }

    public decimal ExportPurchases { get; init; }

    public decimal HomeSalesVat { get; init; }

    public decimal HomePurchasesVat { get; init; }

    public decimal ExportSalesVat { get; init; }

    public decimal ExportPurchasesVat { get; init; }

    public decimal VatAdjustment { get; init; }

    public decimal VatDue { get; init; }
}
