namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Schedules;

/// <summary>
/// Represents the tax calculation summary (SA110), aggregating income tax,
/// capital gains tax, NIC, student loans, payments on account, and final liability.
/// </summary>
public class Sa110
{
    // Income totals
    public decimal TotalIncome { get; set; }
    public decimal TotalTaxableIncome { get; set; }

    // Income tax
    public decimal IncomeTaxDue { get; set; }
    public decimal IncomeTaxPaid { get; set; }
    public decimal IncomeTaxOutstanding { get; set; }

    // Capital gains
    public decimal TotalCapitalGains { get; set; }
    public decimal TaxableCapitalGains { get; set; }
    public decimal CapitalGainsTaxDue { get; set; }

    // NIC (Class 2 & 4)
    public decimal Class2Nic { get; set; }
    public decimal Class4Nic { get; set; }

    // Student loans
    public decimal StudentLoanRepayment { get; set; }
    public decimal PostgraduateLoanRepayment { get; set; }

    // Payments on account
    public decimal PaymentsOnAccountMade { get; set; }
    public decimal PaymentsOnAccountNextYear { get; set; }

    // Final liability
    public decimal TotalTaxDue { get; set; }
    public decimal TotalTaxPaid { get; set; }
    public decimal BalancingPayment { get; set; }
    public decimal RefundDue { get; set; }

    // Convenience
    public string ToXml() => Sa110Serializer.Serialize(this);
}
