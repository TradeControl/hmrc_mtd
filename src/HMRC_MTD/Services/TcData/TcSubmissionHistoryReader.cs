using TradeControl.Tax.UK.Models.Tc;

namespace TradeControl.Tax.UK.Services.TcData;

public sealed class TcSubmissionHistoryReader
{
    public Task<IReadOnlyList<TcSubmissionHistory>> ReadAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<TcSubmissionHistory> rows = Array.Empty<TcSubmissionHistory>();
        return Task.FromResult(rows);
    }
}
