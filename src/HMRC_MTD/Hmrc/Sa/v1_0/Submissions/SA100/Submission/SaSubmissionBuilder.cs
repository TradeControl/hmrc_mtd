using System.Collections.Generic;
using TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.SA100.Schedules;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.SA100.Submission
{
    /// <summary>
    /// Entry point for building a complete SA submission payload.
    /// Constructs the SA envelope, attaches all schedule documents,
    /// and delegates IRmark generation and final XML serialization
    /// to SaEnvelopeBuilder.
    /// </summary>
    public static class SaSubmissionBuilder
    {
        public static string BuildEops(
            string senderId,
            string password,
            Sa100 sa100,
            Sa102 sa102,
            Sa103F sa103f,
            Sa105 sa105,
            Sa106 sa106,
            Sa108 sa108,
            Sa110 sa110)
        {
            SaEnvelopeHeader header = new SaEnvelopeHeader();

            header.SenderDetails.IDAuthentication.SenderID = senderId;
            header.SenderDetails.IDAuthentication.Password = password;

            // Construct envelope
            var envelope = new SaEnvelope
            {
                Header = header,
                Body = new List<SaScheduleDocument>()
            };

            // Attach schedules using existing ToXml() methods
            envelope.Body.Add(new SaScheduleDocument
            {
                Name = Sa100.SA_NAME,
                XmlContent = sa100.ToXml()
            });

            envelope.Body.Add(new SaScheduleDocument
            {
                Name = Sa102.SA_NAME,
                XmlContent = sa102.ToXml()
            });

            envelope.Body.Add(new SaScheduleDocument
            {
                Name = Sa103F.SA_NAME,
                XmlContent = sa103f.ToXml()
            });

            envelope.Body.Add(new SaScheduleDocument
            {
                Name = Sa105.SA_NAME,
                XmlContent = sa105.ToXml()
            });

            envelope.Body.Add(new SaScheduleDocument
            {
                Name = Sa106.SA_NAME,
                XmlContent = sa106.ToXml()
            });

            envelope.Body.Add(new SaScheduleDocument
            {
                Name = Sa108.SA_NAME,
                XmlContent = sa108.ToXml()
            });

            envelope.Body.Add(new SaScheduleDocument
            {
                Name = Sa110.SA_NAME,
                XmlContent = sa110.ToXml()
            });

            // Finalise envelope: canonicalise, IRmark, final XML
            return SaEnvelopeBuilder.Build(envelope);
        }
    }
}
