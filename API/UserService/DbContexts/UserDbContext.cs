using Microsoft.Data.SqlClient;

namespace UserAPI.DbContexts
{
    public class UserDbContext
    {
        private readonly string _connectionString;

        public UserDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
