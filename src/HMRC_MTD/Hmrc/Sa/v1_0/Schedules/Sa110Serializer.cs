using System.Globalization;
using System.Text;
using System.Xml;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Schedules;

/// <summary>
/// Serializes the SA110 tax calculation summary into HMRC‑compliant XML.
/// </summary>
public static class Sa110Serializer
{
    private const string SaNamespace = "http://www.govtalk.gov.uk/taxation/SA/SA110/2023-24";

    public static string Serialize(Sa110 sa110)
    {
        using var stream = new MemoryStream();

        var settings = new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false),
            Indent = false,
            NewLineHandling = NewLineHandling.None,
            OmitXmlDeclaration = false
        };

        using (var writer = XmlWriter.Create(stream, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("SA110", SaNamespace);

            // Income totals
            writer.WriteElementString("TotalIncome", sa110.TotalIncome.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TotalTaxableIncome", sa110.TotalTaxableIncome.ToString("F2", CultureInfo.InvariantCulture));

            // Income tax
            writer.WriteElementString("IncomeTaxDue", sa110.IncomeTaxDue.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("IncomeTaxPaid", sa110.IncomeTaxPaid.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("IncomeTaxOutstanding", sa110.IncomeTaxOutstanding.ToString("F2", CultureInfo.InvariantCulture));

            // Capital gains
            writer.WriteElementString("TotalCapitalGains", sa110.TotalCapitalGains.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TaxableCapitalGains", sa110.TaxableCapitalGains.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("CapitalGainsTaxDue", sa110.CapitalGainsTaxDue.ToString("F2", CultureInfo.InvariantCulture));

            // NIC
            writer.WriteElementString("Class2Nic", sa110.Class2Nic.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Class4Nic", sa110.Class4Nic.ToString("F2", CultureInfo.InvariantCulture));

            // Student loans
            writer.WriteElementString("StudentLoanRepayment", sa110.StudentLoanRepayment.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("PostgraduateLoanRepayment", sa110.PostgraduateLoanRepayment.ToString("F2", CultureInfo.InvariantCulture));

            // Payments on account
            writer.WriteElementString("PaymentsOnAccountMade", sa110.PaymentsOnAccountMade.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("PaymentsOnAccountNextYear", sa110.PaymentsOnAccountNextYear.ToString("F2", CultureInfo.InvariantCulture));

            // Final liability
            writer.WriteElementString("TotalTaxDue", sa110.TotalTaxDue.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TotalTaxPaid", sa110.TotalTaxPaid.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("BalancingPayment", sa110.BalancingPayment.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("RefundDue", sa110.RefundDue.ToString("F2", CultureInfo.InvariantCulture));

            writer.WriteEndElement(); // SA110
            writer.WriteEndDocument();
        }

        return Encoding.UTF8.GetString(stream.ToArray());
    }
}
