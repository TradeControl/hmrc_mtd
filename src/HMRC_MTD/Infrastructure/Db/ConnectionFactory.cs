using Microsoft.Data.SqlClient;

namespace TradeControl.Tax.UK.Infrastructure.Db;

public sealed class ConnectionFactory
{
    public SqlConnection Create(string connectionString)
    {
        return new SqlConnection(connectionString);
    }
}
