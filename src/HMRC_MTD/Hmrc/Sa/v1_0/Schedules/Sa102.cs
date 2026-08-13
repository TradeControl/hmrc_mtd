namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Schedules;

/// <summary>
/// Represents the employment income schedule (SA102), including pay, tax deducted,
/// benefits, expenses, and employer details.
/// </summary>
public class Sa102
{
    // Employer identity
    public string EmployerName { get; set; } = string.Empty;
    public string EmployerPayeReference { get; set; } = string.Empty;

    // Employment period
    public DateTime? EmploymentStartDate { get; set; }
    public DateTime? EmploymentEndDate { get; set; }

    // Core pay/tax
    public decimal Pay { get; set; }
    public decimal TaxTakenOffPay { get; set; }

    // Benefits (P11D-style)
    public decimal BenefitsInKind { get; set; }

    // Phase 2: Detailed benefits
    public decimal CarBenefit { get; set; }
    public decimal FuelBenefit { get; set; }
    public decimal MedicalInsurance { get; set; }
    public decimal EmployerLoans { get; set; }
    public decimal AccommodationBenefit { get; set; }
    public decimal OtherBenefits { get; set; }

    // Allowable expenses
    public decimal AllowableExpenses { get; set; }

    // Lump sums / termination payments
    public decimal RedundancyPayments { get; set; }
    public decimal TerminationPayments { get; set; }
    public decimal TaxableLumpSums { get; set; }

    // Share-based remuneration
    public decimal ShareOptionsTaxed { get; set; }
    public decimal ShareAwardsTaxed { get; set; }

    // Student loans
    public decimal StudentLoanDeducted { get; set; }

    // NICs
    public decimal Class1Nic { get; set; }

    // Off-payroll working
    public bool OffPayrollWorker { get; set; }

    // Foreign employment
    public bool IsForeignEmployment { get; set; }

    // Director flag
    public bool IsCeoOrDirector { get; set; }

    // Convenience method
    public string ToXml() => Sa102Serializer.Serialize(this);
}
