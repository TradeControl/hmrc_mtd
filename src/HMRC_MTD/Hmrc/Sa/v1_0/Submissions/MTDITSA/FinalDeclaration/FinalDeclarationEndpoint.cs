namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.FinalDeclaration
{
    public static class FinalDeclarationEndpoint
    {
        private const string Template = "/income-tax/{mtditid}/final-declaration";

        public static string Path(string mtditid)
            => Template.Replace("{mtditid}", mtditid);

        public const string Method = "POST";
        public const string Version = "1.0";
        public const string Scope = "write:self-assessment";
    }
}
