using Dapper;
using IsconGathiya.Common;
using Npgsql;
using System.Data;

namespace IsconGathiya.Data
{
    public class DapperService : IDapperService
    {
        #region Properties
        //private static readonly string ConnStringInfoCar = ConfigItems.ConnectionStringInfoCar;
        //private static readonly string ConnStringFFM2 = ConfigItems.ConnectionStringFFM2;
        private static readonly string connString = "User ID=postgres;Password=123;Server=localhost;Port=5432;Database=IsconGathiya;Pooling=true;";

        public object DapperHelpers { get; private set; } = null!;

        private readonly int defaultCommandTimeout = 3000;

        #endregion

        #region Methods
        /// <summary>
        /// It will check whether Stored procedure needs to call another read only database or not.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private static string UseConnectionString()
        {
            //if (Enums.ExternalDatabase.Infocar.GetHashCode() == dbName)
            //{
            //    return ConnStringInfoCar;
            //}
            //else
            //{
            //    return ConnStringFFM2;
            //}
            return connString;
        }

   
        public object ExecuteScalar(string commandText, object? param = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, string connectionString = "")
        {
            connectionString = string.IsNullOrEmpty(connectionString) ? ConfigItems.DBConnectionString() : connectionString.Trim();
            using (IDbConnection _db = new NpgsqlConnection(connectionString))
            {
                return _db.ExecuteScalar(commandText,
                                         param: param,
                                         transaction: transaction,
                                         commandTimeout: (commandTimeout > 0 ? commandTimeout : defaultCommandTimeout),
                                         commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> ExecuteQuery<T>(string query, object? param = null, string connectionString = "")
        {
            connectionString = string.IsNullOrEmpty(connectionString) ? ConfigItems.DBConnectionString() : connectionString.Trim();

            using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    var result = connection.Query<T>(query, param).ToList();
                    
                return result;
                }
           
        }

        public object Execute(string commandText, object? param = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, string connectionString = "")
        {
            connectionString = string.IsNullOrEmpty(connectionString) ? ConfigItems.DBConnectionString() : connectionString.Trim();

            using (IDbConnection _db = new NpgsqlConnection(connectionString))
            {
                // Execute the stored procedure
                _db.Execute(commandText,
                            param: param,
                            transaction: transaction,
                            commandTimeout: (commandTimeout > 0 ? commandTimeout : defaultCommandTimeout),
                            commandType: CommandType.StoredProcedure);

                // Assuming 'param' is of type DynamicParameters and contains an output parameter 'success'
                if (param is DynamicParameters dynamicParams)
                {
                    return dynamicParams.Get<bool>("success");
                }

                return null; // Return null if the output parameter is not found
            }
        }

        #endregion
    }
}
