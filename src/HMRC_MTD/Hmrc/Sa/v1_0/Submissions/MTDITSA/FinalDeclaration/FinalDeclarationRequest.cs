using TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.FinalDeclaration
{
    public class FinalDeclarationRequest
    {
        public FinalDeclarationIncomeSummary IncomeSummary { get; set; } = new();
        public FinalDeclarationDeductions Deductions { get; set; } = new();
        public FinalDeclarationTaxCalculation TaxCalculation { get; set; } = new();
        public FinalDeclarationMetadata Metadata { get; set; } = new();
    }
}
