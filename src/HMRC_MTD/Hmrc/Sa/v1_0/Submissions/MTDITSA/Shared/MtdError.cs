namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared
{
    public class MtdError
    {
        public string Code { get; set; } = string.Empty;    // HMRC error code
        public string Message { get; set; } = string.Empty; // Human-readable
        public string Target { get; set; } = string.Empty;  // Field or section
    }
}
