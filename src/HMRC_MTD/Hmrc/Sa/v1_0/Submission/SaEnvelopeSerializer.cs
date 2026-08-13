using System.Text;
using System.Xml;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submission;

/// <summary>
/// Serializes the full SA submission envelope into HMRC‑compliant XML,
/// combining header, schedules, and IRmark.
/// </summary>
public static class SaEnvelopeSerializer
{
    private const string EnvelopeNamespace = "http://www.govtalk.gov.uk/CM/envelope";

    public static string Serialize(SaEnvelope envelope)
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

            // Root element with stable namespace
            writer.WriteStartElement("IRenvelope", EnvelopeNamespace);

            WriteHeader(writer, envelope.Header);

            // IRmark must appear immediately after Header
            writer.WriteElementString("IRmark", envelope.IRmark ?? string.Empty);

            WriteBody(writer, envelope.Body);

            writer.WriteEndElement(); // IRenvelope
            writer.WriteEndDocument();
        }

        return Encoding.UTF8.GetString(stream.ToArray());
    }

    private static void WriteHeader(XmlWriter writer, SaEnvelopeHeader header)
    {
        writer.WriteStartElement("Header");

        // MessageDetails
        writer.WriteStartElement("MessageDetails");
        writer.WriteElementString("Class", header.MessageDetails.Class);
        writer.WriteElementString("Qualifier", header.MessageDetails.Qualifier);
        writer.WriteElementString("Function", header.MessageDetails.Function);
        writer.WriteElementString("TransactionID", header.MessageDetails.TransactionID);
        writer.WriteEndElement(); // MessageDetails

        // SenderDetails
        writer.WriteStartElement("SenderDetails");
        writer.WriteStartElement("IDAuthentication");

        writer.WriteElementString("SenderID", header.SenderDetails.IDAuthentication.SenderID);

        writer.WriteStartElement("Authentication");
        writer.WriteElementString("Method", "clear");
        writer.WriteElementString("Value", header.SenderDetails.IDAuthentication.Password);
        writer.WriteEndElement(); // Authentication

        writer.WriteEndElement(); // IDAuthentication
        writer.WriteEndElement(); // SenderDetails

        writer.WriteEndElement(); // Header
    }

    private static void WriteBody(XmlWriter writer, List<SaScheduleDocument> schedules)
    {
        writer.WriteStartElement("Body");

        foreach (var schedule in schedules)
        {
            // Clean schedule XML before insertion
            var xml = CleanScheduleXml(schedule.XmlContent);

            writer.WriteRaw(xml);
        }

        writer.WriteEndElement(); // Body
    }

    private static string CleanScheduleXml(string xml)
    {
        if (string.IsNullOrWhiteSpace(xml))
            return string.Empty;

        // Remove XML declaration if present
        xml = RemoveXmlDeclaration(xml);

        // Trim whitespace
        xml = xml.Trim();

        // Normalise line endings
        xml = xml.Replace("\r\n", "\n").Replace("\r", "\n");

        return xml;
    }

    private static string RemoveXmlDeclaration(string xml)
    {
        if (xml.StartsWith("<?xml"))
        {
            var end = xml.IndexOf("?>");
            if (end >= 0)
                return xml[(end + 2)..];
        }

        return xml;
    }
}
