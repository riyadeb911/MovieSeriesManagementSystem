using Microsoft.Data.SqlClient;

namespace MovieSeriesManagementSystem.DataBase
{
    public class DbConnection
    {
        private string connectionString =
            @"Server=tcp:localhost,1433;Database=MovieSeriesDB;Integrated Security=True;TrustServerCertificate=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}