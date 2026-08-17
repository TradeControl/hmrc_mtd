namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared
{
    public class MtdMetadata
    {
        public string SubmissionTimestamp { get; set; } = string.Empty; // ISO 8601
        public string CorrelationId { get; set; } = string.Empty;       // Optional
        public string SoftwareId { get; set; } = string.Empty;          // HMRC-issued
        public string SoftwareVersion { get; set; } = string.Empty;
    }
}
