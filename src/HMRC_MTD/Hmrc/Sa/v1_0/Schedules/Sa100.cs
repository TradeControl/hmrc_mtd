namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Schedules;

/// <summary>
/// Represents the main Self Assessment return (SA100), containing taxpayer identity,
/// summary income figures, and core declaration information.
/// </summary>
public class Sa100
{
    // Identity
    public string TaxYear { get; set; } = string.Empty;
    public string Utr { get; set; } = string.Empty;
    public string Nino { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;

    // Contact
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public string AddressLine3 { get; set; } = string.Empty;
    public string Postcode { get; set; } = string.Empty;

    // Business summary
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal AdjustedProfit { get; set; }

    // Tax summary
    public decimal TotalTaxDue { get; set; }
    public decimal TotalTaxPaid { get; set; }
    public decimal TaxOutstanding { get; set; }

    // NI summary
    public decimal Class4Liability { get; set; }
    public decimal Class2Liability { get; set; }

    // Phase 2: Loss summary
    public Sa100LossSummary LossSummary { get; set; } = new();

    // Phase 2: Basis period summary
    public Sa100BasisPeriodSummary BasisPeriodSummary { get; set; } = new();

    // Phase 2: Capital allowances summary
    public Sa100CapitalAllowanceSummary CapitalAllowanceSummary { get; set; } = new();

    // Declaration flags
    public bool IsFinalised { get; set; }

    public string ToXml() => Sa100Serializer.Serialize(this);
}
