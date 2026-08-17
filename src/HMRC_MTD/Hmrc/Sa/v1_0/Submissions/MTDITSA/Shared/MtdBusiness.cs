namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared
{
    public class MtdBusiness
    {
        public string BusinessId { get; set; } = string.Empty;
        public string TypeOfBusiness { get; set; } = string.Empty;
        public string TradingName { get; set; } = string.Empty;

        public DateOnly PeriodStartOn { get; set; }
        public DateOnly PeriodEndOn { get; set; }

        public string AccountingPeriodStart => PeriodStartOn.ToString("yyyy-MM-dd");
        public string AccountingPeriodEnd => PeriodEndOn.ToString("yyyy-MM-dd");
    }

}
