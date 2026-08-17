namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.SA100.Schedules;

/// <summary>
/// Represents the UK property income schedule (SA105), including rental income,
/// allowable expenses, losses, and furnished holiday letting details.
/// </summary>
public class Sa105
{
    public static string SA_NAME { get; } = "SA105";

    // Property identity
    public string PropertyName { get; set; } = string.Empty;
    public bool IsFurnishedHolidayLet { get; set; }

    // Period
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }

    // Income
    public decimal UkPropertyIncome { get; set; }
    public decimal UkFhlIncome { get; set; }

    // Expense headings (UK property)
    public decimal RentRates { get; set; }
    public decimal PropertyRepairs { get; set; }
    public decimal LoanInterest { get; set; }
    public decimal LegalProfessionalFees { get; set; }
    public decimal AgentFees { get; set; }
    public decimal Insurance { get; set; }
    public decimal OtherPropertyExpenses { get; set; }

    // Expense headings (FHL)
    public decimal FhlRepairs { get; set; }
    public decimal FhlLoanInterest { get; set; }
    public decimal FhlAgentFees { get; set; }
    public decimal FhlInsurance { get; set; }
    public decimal FhlOtherExpenses { get; set; }

    // Adjusted figures
    public decimal UkPropertyAdjustedProfit { get; set; }
    public decimal UkFhlAdjustedProfit { get; set; }

    // Losses – UK property
    public decimal UkPropertyLossBroughtForward { get; set; }
    public decimal UkPropertyLossUsedThisYear { get; set; }
    public decimal UkPropertyLossCarriedForward { get; set; }

    // Losses – FHL
    public decimal FhlLossBroughtForward { get; set; }
    public decimal FhlLossUsedThisYear { get; set; }
    public decimal FhlLossCarriedForward { get; set; }

    // Cessation / flags
    public bool HasCeasedLetting { get; set; }

    public string ToXml() => Sa105Serializer.Serialize(this);
}
