using TradeControl.Tax.UK.Models.Hmrc;

namespace TradeControl.Tax.UK.Services.Transport;

public sealed class FraudHeaderService
{
    public FraudHeaders Build()
    {
        return new FraudHeaders();
    }
}
