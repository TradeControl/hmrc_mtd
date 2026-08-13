using System.Globalization;
using System.Text;
using System.Xml;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Schedules;

/// <summary>
/// Serializes the SA100 main return into HMRC‑compliant XML for inclusion
/// within the Self Assessment submission envelope.
/// </summary>
public static class Sa100Serializer
{
    private const string SaNamespace = "http://www.govtalk.gov.uk/taxation/SA/SA100/2023-24";

    public static string Serialize(Sa100 sa100)
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
            writer.WriteStartElement("SA100", SaNamespace);

            // Identity
            writer.WriteElementString("TaxYear", sa100.TaxYear);
            writer.WriteElementString("UTR", sa100.Utr);
            writer.WriteElementString("NINO", sa100.Nino);
            writer.WriteElementString("FullName", sa100.FullName);

            // Address
            writer.WriteStartElement("Address");
            writer.WriteElementString("Line1", sa100.AddressLine1);
            writer.WriteElementString("Line2", sa100.AddressLine2);
            writer.WriteElementString("Line3", sa100.AddressLine3);
            writer.WriteElementString("Postcode", sa100.Postcode);
            writer.WriteEndElement();

            // Business summary
            writer.WriteStartElement("BusinessSummary");
            writer.WriteElementString("TotalIncome", sa100.TotalIncome.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TotalExpenses", sa100.TotalExpenses.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("AdjustedProfit", sa100.AdjustedProfit.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // Tax summary
            writer.WriteStartElement("TaxSummary");
            writer.WriteElementString("TotalTaxDue", sa100.TotalTaxDue.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TotalTaxPaid", sa100.TotalTaxPaid.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TaxOutstanding", sa100.TaxOutstanding.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // NI summary
            writer.WriteStartElement("NISummary");
            writer.WriteElementString("Class4Liability", sa100.Class4Liability.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Class2Liability", sa100.Class2Liability.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // Loss summary
            writer.WriteStartElement("LossSummary");
            writer.WriteElementString("LossBroughtForward", sa100.LossSummary.LossBroughtForward.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossUsedThisYear", sa100.LossSummary.LossUsedThisYear.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossCarriedForward", sa100.LossSummary.LossCarriedForward.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossUsedAgainstOtherIncome", sa100.LossSummary.LossUsedAgainstOtherIncome.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // Basis period summary
            writer.WriteStartElement("BasisPeriodSummary");
            writer.WriteElementString("BasisPeriodStart", sa100.BasisPeriodSummary.BasisPeriodStart.ToString("yyyy-MM-dd"));
            writer.WriteElementString("BasisPeriodEnd", sa100.BasisPeriodSummary.BasisPeriodEnd.ToString("yyyy-MM-dd"));
            writer.WriteElementString("OverlapProfit", sa100.BasisPeriodSummary.OverlapProfit.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("OverlapReliefUsed", sa100.BasisPeriodSummary.OverlapReliefUsed.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TransitionalProfit", sa100.BasisPeriodSummary.TransitionalProfit.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TransitionalRelief", sa100.BasisPeriodSummary.TransitionalRelief.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TransitionalProfitSpread", sa100.BasisPeriodSummary.TransitionalProfitSpread.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // Capital allowances summary
            writer.WriteStartElement("CapitalAllowanceSummary");
            writer.WriteElementString("CapitalAllowancesTotal", sa100.CapitalAllowanceSummary.CapitalAllowancesTotal.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("AnnualInvestmentAllowance", sa100.CapitalAllowanceSummary.AnnualInvestmentAllowance.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("WritingDownAllowance", sa100.CapitalAllowanceSummary.WritingDownAllowance.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("BalancingCharges", sa100.CapitalAllowanceSummary.BalancingCharges.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("BalancingAllowances", sa100.CapitalAllowanceSummary.BalancingAllowances.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            writer.WriteEndElement(); // SA100
            writer.WriteEndDocument();
        }

        return Encoding.UTF8.GetString(stream.ToArray());
    }
}
