namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.SA100.Schedules;

/// <summary>
/// Represents the foreign income schedule (SA106), covering foreign employment,
/// self‑employment, property, investments, pensions, and double‑taxation relief.
/// </summary>
public class Sa106
{
    public static string SA_NAME { get; } = "SA106";

    // Foreign employment
    public decimal ForeignEmploymentIncome { get; set; }
    public decimal ForeignEmploymentTaxPaid { get; set; }
    public decimal ForeignEmploymentBenefits { get; set; }
    public decimal ForeignEmploymentSocialSecurity { get; set; }
    public decimal ForeignEmploymentLumpSums { get; set; }
    public decimal ForeignEmploymentShareBased { get; set; }

    // Foreign self-employment
    public decimal ForeignSelfEmploymentIncome { get; set; }
    public decimal ForeignSelfEmploymentExpenses { get; set; }
    public decimal ForeignSelfEmploymentTaxPaid { get; set; }
    public decimal ForeignSelfEmploymentLossBroughtForward { get; set; }
    public decimal ForeignSelfEmploymentLossUsedThisYear { get; set; }
    public decimal ForeignSelfEmploymentLossCarriedForward { get; set; }

    // Foreign property
    public decimal ForeignPropertyIncome { get; set; }
    public decimal ForeignPropertyExpenses { get; set; }
    public decimal ForeignPropertyTaxPaid { get; set; }
    public decimal ForeignPropertyLossBroughtForward { get; set; }
    public decimal ForeignPropertyLossUsedThisYear { get; set; }
    public decimal ForeignPropertyLossCarriedForward { get; set; }

    // Foreign interest and dividends
    public decimal ForeignInterestIncome { get; set; }
    public decimal ForeignGovernmentBondInterest { get; set; }
    public decimal ForeignCorporateBondInterest { get; set; }
    public decimal ForeignDividendIncome { get; set; }
    public decimal ForeignReitDistributions { get; set; }
    public decimal ForeignInvestmentTaxPaid { get; set; }

    // Foreign pensions and social security
    public decimal ForeignStatePension { get; set; }
    public decimal ForeignPrivatePension { get; set; }
    public decimal ForeignPensionLumpSums { get; set; }
    public decimal ForeignPensionTaxPaid { get; set; }
    public decimal ForeignSocialSecurityRefunds { get; set; }

    // Double taxation relief (summary)
    public decimal DoubleTaxationReliefClaimed { get; set; }

    // Country-by-country breakdown (simplified structural hook)
    public List<Sa106CountryBreakdown> CountryBreakdowns { get; set; } = new();

    // Convenience
    public string ToXml() => Sa106Serializer.Serialize(this);
}

/// <summary>
/// Foreign income schedule by country
/// </summary>
public class Sa106CountryBreakdown
{
    public string CountryCode { get; set; } = string.Empty;   // e.g. "US", "FR"
    public string IncomeType { get; set; } = string.Empty;    // e.g. "employment", "dividends"
    public decimal IncomeAmount { get; set; }
    public decimal ForeignTaxPaid { get; set; }
    public string TreatyArticle { get; set; } = string.Empty; // optional
    public string ReliefMethod { get; set; } = string.Empty;  // e.g. "credit", "exemption"
}
