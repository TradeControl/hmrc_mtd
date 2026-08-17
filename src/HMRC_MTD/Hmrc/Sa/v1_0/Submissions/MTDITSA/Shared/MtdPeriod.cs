namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared
{
    namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.MTDITSA.Shared
    {
        public class MtdPeriod
        {
            private readonly DateOnly _periodStartOn;
            private readonly DateOnly _periodEndOn;

            public MtdPeriod(DateOnly periodStartOn, DateOnly periodEndOn)
            {
                _periodStartOn = periodStartOn;
                _periodEndOn = periodEndOn;
            }

            public string PeriodStart => _periodStartOn.ToString("yyyy-MM-dd");
            public string PeriodEnd => _periodEndOn.ToString("yyyy-MM-dd");
        }
    }
}
