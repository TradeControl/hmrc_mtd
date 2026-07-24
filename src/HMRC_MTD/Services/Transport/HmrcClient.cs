namespace TradeControl.Tax.UK.Services.Transport;

public sealed class HmrcClient
{
    public Task<object?> SubmitAsync(object payload, CancellationToken cancellationToken = default)
    {
        object response = new
        {
            mode = "not_implemented",
            message = "HMRC transport is outside Objective 2 scope."
        };

        return Task.FromResult<object?>(response);
    }
}
