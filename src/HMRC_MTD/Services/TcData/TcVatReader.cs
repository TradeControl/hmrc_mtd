using Microsoft.Data.SqlClient;
using TradeControl.Tax.UK.Infrastructure.Db;
using TradeControl.Tax.UK.Models.Tc;

namespace TradeControl.Tax.UK.Services.TcData;

public sealed class TcVatReader
{
    private readonly ConnectionFactory _connectionFactory;

    public TcVatReader(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<TcVatStatement?> ReadAsync(
        string connectionString,
        DateTime periodEndOn,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
SELECT YearNumber,
       Description,
       Period,
       StartOn,
       HomeSales,
       HomePurchases,
       ExportSales,
       ExportPurchases,
       HomeSalesVat,
       HomePurchasesVat,
       ExportSalesVat,
       ExportPurchasesVat,
       VatAdjustment,
       VatDue
FROM Cash.vwTaxVatTotals
WHERE StartOn = @StartOn;
""";

        using var connection = _connectionFactory.Create(connectionString);
        await SqlHelpers.EnsureOpenAsync(connection, cancellationToken);

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@StartOn", periodEndOn);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new TcVatStatement
        {
            YearNumber = SqlHelpers.GetInt32(reader, "YearNumber"),
            Description = SqlHelpers.GetString(reader, "Description"),
            Period = SqlHelpers.GetString(reader, "Period"),
            StartOn = SqlHelpers.GetDateTime(reader, "StartOn"),
            HomeSales = SqlHelpers.GetDecimal(reader, "HomeSales"),
            HomePurchases = SqlHelpers.GetDecimal(reader, "HomePurchases"),
            ExportSales = SqlHelpers.GetDecimal(reader, "ExportSales"),
            ExportPurchases = SqlHelpers.GetDecimal(reader, "ExportPurchases"),
            HomeSalesVat = SqlHelpers.GetDecimal(reader, "HomeSalesVat"),
            HomePurchasesVat = SqlHelpers.GetDecimal(reader, "HomePurchasesVat"),
            ExportSalesVat = SqlHelpers.GetDecimal(reader, "ExportSalesVat"),
            ExportPurchasesVat = SqlHelpers.GetDecimal(reader, "ExportPurchasesVat"),
            VatAdjustment = SqlHelpers.GetDecimal(reader, "VatAdjustment"),
            VatDue = SqlHelpers.GetDecimal(reader, "VatDue")
        };
    }
}
