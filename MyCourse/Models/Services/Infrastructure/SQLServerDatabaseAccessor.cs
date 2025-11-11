using Microsoft.Data.SqlClient;
using MyCourse.Models.Enums;
using MyCourse.Models.ValueObjects;
using System.Data;

namespace MyCourse.Models.Services.Infrastructure
{
    public class SQLServerDatabaseAccessor : IDatabaseAccessor
    {
        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {
            //Creiamo dei SqliteParameter a partire dalla FormattableString
            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameters = new List<SqlParameter>();
            for (var i = 0; i < queryArguments.Length; i++)
            {
                var parameter = new SqlParameter(i.ToString(), queryArguments[i]);
                sqliteParameters.Add(parameter);
            }
            string query = formattableQuery.ToString();

            using (SqlConnection conn = new SqlConnection("Server=(localdb)\\MyLocalDB;Database=MyCourse;Trusted_Connection=True;"))
            {
                await conn.OpenAsync();
                using (var comm = new SqlCommand(query, conn))
                {
                    using (var reader = await comm.ExecuteReaderAsync())
                    {
                        DataSet dataSet = new DataSet();
                        do
                        {
                            DataTable dt = new DataTable();
                            dataSet.Tables.Add(dt);
                            dt.Load(reader);
                        } while (!reader.IsClosed);
                        return dataSet;
                    }
                }
            }
        }
    }
}
