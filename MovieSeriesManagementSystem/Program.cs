using Microsoft.Data.SqlClient;
using MovieSeriesManagementSystem.DataBase;

class Program
{
    static void Main()
    {
        DbConnection db = new DbConnection();

        try
        {
            using (SqlConnection connection = db.GetConnection())
            {
                connection.Open();

                Console.WriteLine("Database Connected Successfully!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Database Connection Failed!");
            Console.WriteLine(ex.Message);
        }
    }
}