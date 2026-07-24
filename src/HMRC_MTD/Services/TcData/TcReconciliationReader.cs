using TradeControl.Tax.UK.Models.Tc;

namespace TradeControl.Tax.UK.Services.TcData;

public sealed class TcReconciliationReader
{
    public Task<IReadOnlyList<TcReconciliation>> ReadAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<TcReconciliation> rows = Array.Empty<TcReconciliation>();
        return Task.FromResult(rows);
    }
}
