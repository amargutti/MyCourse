using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCourse.Models.Enums;
using MyCourse.Models.Options;
using MyCourse.Models.ValueObjects;
using System.Data;

namespace MyCourse.Models.Services.Infrastructure
{
    public class SQLServerDatabaseAccessor : IDatabaseAccessor
    {
        private readonly IOptionsMonitor<ConnectionStringOptions> connectionStringOptions;
        private readonly ILogger<SQLServerDatabaseAccessor> logger;

        public SQLServerDatabaseAccessor(IOptionsMonitor<ConnectionStringOptions> connectionStringOptions, ILogger<SQLServerDatabaseAccessor> logger)
        {
            this.connectionStringOptions = connectionStringOptions;
            this.logger = logger;
        }

        public async Task<int> CommandAsync(FormattableString command)
        {
            using SqlConnection conn = await GetOpenedConnection();
            using var comm = GetCommand(command, conn);
            //TODO...
            int affectedRows = await comm.ExecuteNonQueryAsync();
            return affectedRows;
        }

        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {
            using SqlConnection conn = await GetOpenedConnection();
            using var comm = GetCommand(formattableQuery, conn);

            using var reader = await comm.ExecuteReaderAsync();
            DataSet dataSet = new DataSet();
            do
            {
                DataTable dt = new DataTable();
                dataSet.Tables.Add(dt);
                dt.Load(reader);
            } while (!reader.IsClosed);
            return dataSet;
        }

        public async Task<T> QueryScalarAsync<T>(FormattableString query)
        {
            using SqlConnection conn = await GetOpenedConnection();
            using var comm = GetCommand(query, conn);
            //TODO...
            object result = await comm.ExecuteScalarAsync();
            return (T) Convert.ChangeType(result, typeof(T));
        }

        private async Task<SqlConnection> GetOpenedConnection()
        {
            string connectionString = connectionStringOptions.CurrentValue.Default;
            var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();
            return conn;
        }

        private static SqlCommand GetCommand(FormattableString formattableQuery, SqlConnection conn)
        {
            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameters = new List<SqlParameter>();
            for (var i = 0; i < queryArguments.Length; i++)
            {
                if (queryArguments[i] is Sql)
                {
                    continue;
                }
                var parameter = new SqlParameter(i.ToString(), queryArguments[i] ?? DBNull.Value);
                sqliteParameters.Add(parameter);
            }
            string query = formattableQuery.ToString();
            var cmd = new SqlCommand(query, conn);
            return cmd;
        }
    }
}
