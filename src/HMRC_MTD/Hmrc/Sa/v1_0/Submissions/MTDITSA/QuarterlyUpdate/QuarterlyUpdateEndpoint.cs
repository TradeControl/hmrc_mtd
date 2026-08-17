namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.QuarterlyUpdate
{
    public static class QuarterlyUpdateEndpoint
    {
        private const string Template = "/income-tax/{mtditid}/periodic-summary";

        public static string Path(string mtditid)
            => Template.Replace("{mtditid}", mtditid);

        public const string Method = "POST";
        public const string Version = "1.0";
        public const string Scope = "write:self-assessment";
    }

}
