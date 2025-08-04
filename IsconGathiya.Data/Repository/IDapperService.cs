using Npgsql;

namespace IsconGathiya.Data
{
    public interface IDapperService
    {
        object ExecuteScalar(string commandText, object? param = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, string connectionString = "");

        IEnumerable<T> ExecuteQuery<T>(string query, object? param = null, string connectionString = "");
        object Execute(string commandText, object? param = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, string connectionString = "");
    }
}
