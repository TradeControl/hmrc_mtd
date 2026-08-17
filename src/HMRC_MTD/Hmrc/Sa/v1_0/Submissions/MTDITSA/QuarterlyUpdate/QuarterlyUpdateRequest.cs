using TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared;
using TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared.TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.QuarterlyUpdate
{
    public class QuarterlyUpdateRequest
    {
        public MtdBusiness Business { get; set; } = new MtdBusiness();
        public MtdPeriod Period { get; set; } = new MtdPeriod(DateOnly.MinValue, DateOnly.MinValue);

        public List<MtdIncomeCategory> Income { get; set; } = new();
        public List<MtdExpenseCategory> Expenses { get; set; } = new();
        public List<MtdAdjustment> Adjustments { get; set; } = new();

        public MtdMetadata Metadata { get; set; } = new MtdMetadata();
    }
}
