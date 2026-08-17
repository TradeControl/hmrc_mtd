using System.Globalization;
using System.Text;
using System.Xml;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.SA100.Schedules;

/// <summary>
/// Serializes the SA102 employment schedule into HMRC‑compliant XML.
/// </summary>
public static class Sa102Serializer
{
    private const string SaNamespace = "http://www.govtalk.gov.uk/taxation/SA/SA102/2023-24";

    public static string Serialize(Sa102 sa102)
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
            writer.WriteStartElement("SA102", SaNamespace);

            // Employer identity
            writer.WriteElementString("EmployerName", sa102.EmployerName);
            writer.WriteElementString("EmployerPayeReference", sa102.EmployerPayeReference);

            // Employment period
            if (sa102.EmploymentStartDate.HasValue)
                writer.WriteElementString("EmploymentStartDate", sa102.EmploymentStartDate.Value.ToString("yyyy-MM-dd"));

            if (sa102.EmploymentEndDate.HasValue)
                writer.WriteElementString("EmploymentEndDate", sa102.EmploymentEndDate.Value.ToString("yyyy-MM-dd"));

            // Core pay/tax
            writer.WriteElementString("Pay", sa102.Pay.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TaxTakenOffPay", sa102.TaxTakenOffPay.ToString("F2", CultureInfo.InvariantCulture));

            // Benefits summary
            writer.WriteElementString("BenefitsInKind", sa102.BenefitsInKind.ToString("F2", CultureInfo.InvariantCulture));

            // Detailed benefits
            writer.WriteStartElement("Benefits");
            writer.WriteElementString("CarBenefit", sa102.CarBenefit.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("FuelBenefit", sa102.FuelBenefit.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("MedicalInsurance", sa102.MedicalInsurance.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("EmployerLoans", sa102.EmployerLoans.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("AccommodationBenefit", sa102.AccommodationBenefit.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("OtherBenefits", sa102.OtherBenefits.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // Allowable expenses
            writer.WriteElementString("AllowableExpenses", sa102.AllowableExpenses.ToString("F2", CultureInfo.InvariantCulture));

            // Lump sums
            writer.WriteStartElement("LumpSums");
            writer.WriteElementString("RedundancyPayments", sa102.RedundancyPayments.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TerminationPayments", sa102.TerminationPayments.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TaxableLumpSums", sa102.TaxableLumpSums.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // Share-based remuneration
            writer.WriteStartElement("ShareBasedRemuneration");
            writer.WriteElementString("ShareOptionsTaxed", sa102.ShareOptionsTaxed.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("ShareAwardsTaxed", sa102.ShareAwardsTaxed.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // Student loans
            writer.WriteElementString("StudentLoanDeducted", sa102.StudentLoanDeducted.ToString("F2", CultureInfo.InvariantCulture));

            // NICs
            writer.WriteElementString("Class1Nic", sa102.Class1Nic.ToString("F2", CultureInfo.InvariantCulture));

            // Flags
            writer.WriteElementString("OffPayrollWorker", sa102.OffPayrollWorker ? "true" : "false");
            writer.WriteElementString("IsForeignEmployment", sa102.IsForeignEmployment ? "true" : "false");
            writer.WriteElementString("IsCeoOrDirector", sa102.IsCeoOrDirector ? "true" : "false");

            writer.WriteEndElement(); // SA102
            writer.WriteEndDocument();
        }

        return Encoding.UTF8.GetString(stream.ToArray());
    }
}
