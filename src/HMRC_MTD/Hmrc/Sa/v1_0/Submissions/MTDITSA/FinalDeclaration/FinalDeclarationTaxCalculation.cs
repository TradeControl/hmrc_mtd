namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.FinalDeclaration
{
    public class FinalDeclarationTaxCalculation
    {
        public decimal IncomeTaxDue { get; set; }
        public decimal Class4NicDue { get; set; }
        public decimal TotalTaxDue { get; set; }
        public decimal TaxAlreadyPaid { get; set; }
        public decimal TaxOutstanding { get; set; }
    }
}
