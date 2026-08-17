using System.Globalization;
using System.Text;
using System.Xml;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.SA100.Schedules;

/// <summary>
/// Serializes the SA105 UK property schedule into HMRC‑compliant XML.
/// </summary>
public static class Sa105Serializer
{
    private const string SaNamespace = "http://www.govtalk.gov.uk/taxation/SA/SA105/2023-24";

    public static string Serialize(Sa105 sa105)
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
            writer.WriteStartElement("SA105", SaNamespace);

            writer.WriteElementString("PropertyName", sa105.PropertyName);
            writer.WriteElementString("IsFurnishedHolidayLet", sa105.IsFurnishedHolidayLet ? "true" : "false");

            writer.WriteStartElement("Period");
            writer.WriteElementString("Start", sa105.PeriodStart.ToString("yyyy-MM-dd"));
            writer.WriteElementString("End", sa105.PeriodEnd.ToString("yyyy-MM-dd"));
            writer.WriteEndElement();

            writer.WriteStartElement("Income");
            writer.WriteElementString("UkPropertyIncome", sa105.UkPropertyIncome.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("UkFhlIncome", sa105.UkFhlIncome.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            writer.WriteStartElement("UkPropertyExpenses");
            writer.WriteElementString("RentRates", sa105.RentRates.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("PropertyRepairs", sa105.PropertyRepairs.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LoanInterest", sa105.LoanInterest.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LegalProfessionalFees", sa105.LegalProfessionalFees.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("AgentFees", sa105.AgentFees.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Insurance", sa105.Insurance.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("OtherPropertyExpenses", sa105.OtherPropertyExpenses.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            writer.WriteStartElement("FhlExpenses");
            writer.WriteElementString("FhlRepairs", sa105.FhlRepairs.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("FhlLoanInterest", sa105.FhlLoanInterest.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("FhlAgentFees", sa105.FhlAgentFees.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("FhlInsurance", sa105.FhlInsurance.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("FhlOtherExpenses", sa105.FhlOtherExpenses.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            writer.WriteStartElement("AdjustedProfits");
            writer.WriteElementString("UkPropertyAdjustedProfit", sa105.UkPropertyAdjustedProfit.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("UkFhlAdjustedProfit", sa105.UkFhlAdjustedProfit.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            writer.WriteStartElement("Losses");
            writer.WriteElementString("UkPropertyLossBroughtForward", sa105.UkPropertyLossBroughtForward.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("UkPropertyLossUsedThisYear", sa105.UkPropertyLossUsedThisYear.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("UkPropertyLossCarriedForward", sa105.UkPropertyLossCarriedForward.ToString("F2", CultureInfo.InvariantCulture));

            writer.WriteElementString("FhlLossBroughtForward", sa105.FhlLossBroughtForward.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("FhlLossUsedThisYear", sa105.FhlLossUsedThisYear.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("FhlLossCarriedForward", sa105.FhlLossCarriedForward.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            writer.WriteStartElement("Flags");
            writer.WriteElementString("HasCeasedLetting", sa105.HasCeasedLetting ? "true" : "false");
            writer.WriteEndElement();

            writer.WriteEndElement(); // SA105
            writer.WriteEndDocument();
        }

        return Encoding.UTF8.GetString(stream.ToArray());
    }
}
