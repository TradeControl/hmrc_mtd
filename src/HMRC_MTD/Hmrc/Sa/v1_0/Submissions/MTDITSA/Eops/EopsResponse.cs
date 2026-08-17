using TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Eops
{
    public class EopsResponse
    {
        public string SubmissionId { get; set; } = string.Empty;
        public string ProcessingDate { get; set; } = string.Empty;

        public List<MtdError> Errors { get; set; } = new();
    }
}
