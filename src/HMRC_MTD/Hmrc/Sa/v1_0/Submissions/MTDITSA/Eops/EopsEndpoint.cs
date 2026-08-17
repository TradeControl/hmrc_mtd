namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Eops
{
    public static class EopsEndpoint
    {
        private const string Template = "/income-tax/{mtditid}/annual-summary";

        public static string Path(string mtditid)
            => Template.Replace("{mtditid}", mtditid);

        public const string Method = "PUT";
        public const string Version = "1.0";
        public const string Scope = "write:self-assessment";
    }
}
