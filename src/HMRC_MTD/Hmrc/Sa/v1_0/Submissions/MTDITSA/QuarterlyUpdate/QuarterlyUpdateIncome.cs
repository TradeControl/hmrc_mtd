using TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.QuarterlyUpdate
{
    public class QuarterlyUpdateIncome
    {
        public List<MtdIncomeCategory> Items { get; set; } = new();
    }
}
