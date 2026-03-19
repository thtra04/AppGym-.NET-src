using Microsoft.Data.SqlClient;

namespace AppGym.DataAccess
{
    public static class DatabaseHelper
    {
        private static string _connectionString =
            @"Server=(local)\SQLEXPRESS;Database=GymManagementDB;Integrated Security=True;TrustServerCertificate=True;";

        public static string ConnectionString
        {
            get => _connectionString;
            set => _connectionString = value;
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
