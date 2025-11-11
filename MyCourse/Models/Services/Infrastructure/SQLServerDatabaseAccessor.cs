using Microsoft.Data.SqlClient;
using MyCourse.Models.Enums;
using MyCourse.Models.ValueObjects;
using System.Data;

namespace MyCourse.Models.Services.Infrastructure
{
    public class SQLServerDatabaseAccessor : IDatabaseAccessor
    {
        public DataSet Query(string query)
        {
            using (SqlConnection conn = new SqlConnection("Server=(localdb)\\MyLocalDB;Database=MyCourse;Trusted_Connection=True;"))
            {
                conn.Open();
                using (var comm = new SqlCommand(query, conn))
                {
                    using (var reader = comm.ExecuteReader())
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
