namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared
{
    public class MtdIncomeCategory
    {
        public string CategoryName { get; set; } = string.Empty;    // e.g. "turnover"
        public decimal Amount { get; set; }
    }
}
