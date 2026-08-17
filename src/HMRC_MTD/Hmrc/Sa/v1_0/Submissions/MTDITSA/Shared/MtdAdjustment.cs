namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared
{
    public class MtdAdjustment
    {
        public string Reason { get; set; } = string.Empty;  // e.g. "private use adjustment"
        public decimal Amount { get; set; }
    }
}
