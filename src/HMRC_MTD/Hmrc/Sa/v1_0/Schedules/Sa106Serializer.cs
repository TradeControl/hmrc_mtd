using System.Globalization;
using System.Text;
using System.Xml;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Schedules;

/// <summary>
/// Serializes the SA106 foreign income schedule into HMRC‑compliant XML.
/// </summary>
public static class Sa106Serializer
{
    private const string SaNamespace = "http://www.govtalk.gov.uk/taxation/SA/SA106/2023-24";

    public static string Serialize(Sa106 sa106)
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
            writer.WriteStartElement("SA106", SaNamespace);

            // Foreign employment
            writer.WriteStartElement("ForeignEmployment");
            writer.WriteElementString("Income", sa106.ForeignEmploymentIncome.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TaxPaid", sa106.ForeignEmploymentTaxPaid.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Benefits", sa106.ForeignEmploymentBenefits.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("SocialSecurity", sa106.ForeignEmploymentSocialSecurity.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LumpSums", sa106.ForeignEmploymentLumpSums.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("ShareBased", sa106.ForeignEmploymentShareBased.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // Foreign self-employment
            writer.WriteStartElement("ForeignSelfEmployment");
            writer.WriteElementString("Income", sa106.ForeignSelfEmploymentIncome.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Expenses", sa106.ForeignSelfEmploymentExpenses.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TaxPaid", sa106.ForeignSelfEmploymentTaxPaid.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossBroughtForward", sa106.ForeignSelfEmploymentLossBroughtForward.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossUsedThisYear", sa106.ForeignSelfEmploymentLossUsedThisYear.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossCarriedForward", sa106.ForeignSelfEmploymentLossCarriedForward.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // Foreign property
            writer.WriteStartElement("ForeignProperty");
            writer.WriteElementString("Income", sa106.ForeignPropertyIncome.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Expenses", sa106.ForeignPropertyExpenses.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TaxPaid", sa106.ForeignPropertyTaxPaid.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossBroughtForward", sa106.ForeignPropertyLossBroughtForward.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossUsedThisYear", sa106.ForeignPropertyLossUsedThisYear.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossCarriedForward", sa106.ForeignPropertyLossCarriedForward.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // Foreign investments
            writer.WriteStartElement("ForeignInvestments");
            writer.WriteElementString("InterestIncome", sa106.ForeignInterestIncome.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("GovernmentBondInterest", sa106.ForeignGovernmentBondInterest.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("CorporateBondInterest", sa106.ForeignCorporateBondInterest.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("DividendIncome", sa106.ForeignDividendIncome.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("ReitDistributions", sa106.ForeignReitDistributions.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TaxPaid", sa106.ForeignInvestmentTaxPaid.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // Foreign pensions
            writer.WriteStartElement("ForeignPensions");
            writer.WriteElementString("StatePension", sa106.ForeignStatePension.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("PrivatePension", sa106.ForeignPrivatePension.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("PensionLumpSums", sa106.ForeignPensionLumpSums.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TaxPaid", sa106.ForeignPensionTaxPaid.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("SocialSecurityRefunds", sa106.ForeignSocialSecurityRefunds.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // Double taxation relief
            writer.WriteStartElement("DoubleTaxationRelief");
            writer.WriteElementString("ReliefClaimed", sa106.DoubleTaxationReliefClaimed.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // Country breakdowns
            writer.WriteStartElement("CountryBreakdowns");
            foreach (var cb in sa106.CountryBreakdowns)
            {
                writer.WriteStartElement("Country");
                writer.WriteElementString("CountryCode", cb.CountryCode);
                writer.WriteElementString("IncomeType", cb.IncomeType);
                writer.WriteElementString("IncomeAmount", cb.IncomeAmount.ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteElementString("ForeignTaxPaid", cb.ForeignTaxPaid.ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteElementString("TreatyArticle", cb.TreatyArticle);
                writer.WriteElementString("ReliefMethod", cb.ReliefMethod);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteEndElement(); // SA106
            writer.WriteEndDocument();
        }

        return Encoding.UTF8.GetString(stream.ToArray());
    }
}
