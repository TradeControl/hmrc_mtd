namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.FinalDeclaration
{
    public class FinalDeclarationIncomeSummary
    {
        public decimal BusinessProfits { get; set; }
        public decimal PropertyIncome { get; set; }
        public decimal EmploymentIncome { get; set; }
        public decimal PensionIncome { get; set; }
        public decimal Interest { get; set; }
        public decimal Dividends { get; set; }
        public decimal OtherIncome { get; set; }
    }
}
