using Microsoft.Data.SqlClient;
using TradeControl.Tax.UK.Infrastructure.Db;
using TradeControl.Tax.UK.Models.Tc;

namespace TradeControl.Tax.UK.Services.TcData;

public sealed class TcBusinessTaxReader
{
    private readonly ConnectionFactory _connectionFactory;

    public TcBusinessTaxReader(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyList<TcBusinessTaxView>> ReadAsync(
        string connectionString,
        string taxSourceCode,
        DateTime periodTo,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT TaxSourceCode,
       TagCode,
       PeriodFrom,
       PeriodTo,
       TaxableAmount
FROM Cash.vwTaxBizSubmission
WHERE TaxSourceCode = @TaxSourceCode
  AND PeriodTo = @PeriodTo
ORDER BY TagCode;
""";

        using var connection = _connectionFactory.Create(connectionString);
        await SqlHelpers.EnsureOpenAsync(connection, cancellationToken);

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@TaxSourceCode", taxSourceCode);
        command.Parameters.AddWithValue("@PeriodTo", periodTo);

        var rows = new List<TcBusinessTaxView>();

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            rows.Add(new TcBusinessTaxView
            {
                TaxSourceCode = SqlHelpers.GetString(reader, "TaxSourceCode"),
                TagCode = SqlHelpers.GetString(reader, "TagCode"),
                PeriodFrom = SqlHelpers.GetDateTime(reader, "PeriodFrom"),
                PeriodTo = SqlHelpers.GetDateTime(reader, "PeriodTo"),
                TaxableAmount = Math.Abs(SqlHelpers.GetDecimal(reader, "TaxableAmount"))
            });
        }

        return rows;
    }
}
