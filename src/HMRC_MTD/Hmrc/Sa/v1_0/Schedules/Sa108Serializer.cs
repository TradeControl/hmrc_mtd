using System.Globalization;
using System.Text;
using System.Xml;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Schedules;

/// <summary>
/// Serializes the SA108 capital gains schedule into HMRC‑compliant XML,
/// including disposal details and CGT summary totals.
/// </summary>
public static class Sa108Serializer
{
    private const string SaNamespace = "http://www.govtalk.gov.uk/taxation/SA/SA108/2023-24";

    public static string Serialize(Sa108 sa108)
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
            writer.WriteStartElement("SA108", SaNamespace);

            // -------------------------
            // Disposals
            // -------------------------
            writer.WriteStartElement("Disposals");

            foreach (var d in sa108.Disposals)
            {
                writer.WriteStartElement("Disposal");

                writer.WriteElementString("AssetTypeCode", ((byte)d.AssetTypeCode).ToString());
                writer.WriteElementString("AssetType", d.AssetType);
                writer.WriteElementString("Description", d.Description);

                writer.WriteElementString("AcquisitionDate", d.AcquisitionDate.ToString("yyyy-MM-dd"));
                writer.WriteElementString("DisposalDate", d.DisposalDate.ToString("yyyy-MM-dd"));

                writer.WriteElementString("DisposalProceeds", d.DisposalProceeds.ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteElementString("AcquisitionCost", d.AcquisitionCost.ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteElementString("EnhancementCosts", d.EnhancementCosts.ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteElementString("IncidentalCosts", d.IncidentalCosts.ToString("F2", CultureInfo.InvariantCulture));

                writer.WriteElementString("Gain", d.Gain.ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteElementString("Loss", d.Loss.ToString("F2", CultureInfo.InvariantCulture));

                // Reliefs
                writer.WriteStartElement("Reliefs");
                writer.WriteElementString("PrivateResidenceRelief", d.PrivateResidenceRelief.ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteElementString("LettingsRelief", d.LettingsRelief.ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteElementString("BusinessAssetDisposalRelief", d.BusinessAssetDisposalRelief.ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteElementString("RolloverRelief", d.RolloverRelief.ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteElementString("HoldoverRelief", d.HoldoverRelief.ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteElementString("InvestorRelief", d.InvestorRelief.ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteEndElement();

                // Pooling
                writer.WriteStartElement("Pooling");
                writer.WriteElementString("IsSection104Pool", d.IsSection104Pool ? "true" : "false");
                writer.WriteElementString("PoolAllowableCost", d.PoolAllowableCost.ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteEndElement();

                // Foreign
                writer.WriteStartElement("Foreign");
                writer.WriteElementString("ForeignTaxPaid", d.ForeignTaxPaid.ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteElementString("CountryCode", d.CountryCode);
                writer.WriteEndElement();

                // Remittance basis
                writer.WriteElementString("IsRemitted", d.IsRemitted ? "true" : "false");

                writer.WriteEndElement(); // Disposal
            }

            writer.WriteEndElement(); // Disposals

            // -------------------------
            // Losses
            // -------------------------
            writer.WriteStartElement("Losses");
            writer.WriteElementString("LossesBroughtForward", sa108.LossesBroughtForward.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossesUsedThisYear", sa108.LossesUsedThisYear.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossesCarriedForward", sa108.LossesCarriedForward.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // -------------------------
            // Annual Exempt Amount
            // -------------------------
            writer.WriteElementString("AnnualExemptAmount", sa108.AnnualExemptAmount.ToString("F2", CultureInfo.InvariantCulture));

            // -------------------------
            // Gains by rate bands
            // -------------------------
            writer.WriteStartElement("RateBands");
            writer.WriteElementString("GainsAt10", sa108.GainsAt10.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("GainsAt20", sa108.GainsAt20.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("GainsAt18", sa108.GainsAt18.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("GainsAt28", sa108.GainsAt28.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // -------------------------
            // Totals
            // -------------------------
            writer.WriteElementString("TotalGains", sa108.TotalGains.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TaxableGains", sa108.TaxableGains.ToString("F2", CultureInfo.InvariantCulture));

            // -------------------------
            // Foreign gains
            // -------------------------
            writer.WriteStartElement("ForeignGains");
            writer.WriteElementString("ForeignGainsAmount", sa108.ForeignGains.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("ForeignTaxPaid", sa108.ForeignTaxPaid.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("ForeignDoubleTaxRelief", sa108.ForeignDoubleTaxRelief.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // -------------------------
            // Remittance basis
            // -------------------------
            writer.WriteStartElement("RemittanceBasis");
            writer.WriteElementString("GainsNotRemitted", sa108.GainsNotRemitted.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("GainsRemitted", sa108.GainsRemitted.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            writer.WriteEndElement(); // SA108
            writer.WriteEndDocument();
        }

        return Encoding.UTF8.GetString(stream.ToArray());
    }
}
