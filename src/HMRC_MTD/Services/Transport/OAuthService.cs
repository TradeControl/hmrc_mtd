namespace TradeControl.Tax.UK.Services.Transport;

public sealed class OAuthService
{
    public Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<string?>(null);
    }
}
