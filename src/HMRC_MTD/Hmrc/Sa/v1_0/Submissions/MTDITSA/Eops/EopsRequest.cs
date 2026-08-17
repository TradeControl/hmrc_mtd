using TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared;
using TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared.TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Eops
{
    public class EopsRequest
    {
        public MtdBusiness Business { get; set; } = new MtdBusiness();
        public MtdPeriod Period { get; set; } = new MtdPeriod(DateOnly.MinValue, DateOnly.MinValue);

        public EopsAllowances Allowances { get; set; } = new EopsAllowances();
        public EopsLosses Losses { get; set; } = new EopsLosses();
        public EopsAdjustments Adjustments { get; set; } = new EopsAdjustments();

        public EopsMetadata Metadata { get; set; } = new EopsMetadata();
    }
}
