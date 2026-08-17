namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared
{
    public class MtdExpenseCategory
    {
        public string CategoryName { get; set; } = string.Empty;    // e.g. "repairs"
        public decimal Amount { get; set; }
        public bool IsDisallowable { get; set; }                    // HMRC split
    }
}
