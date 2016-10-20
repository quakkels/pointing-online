using System.Data;
using System.Data.SqlClient;

namespace PointingPoker.DataAccess
{
    public class DbConnectionProvider : IDbConnectionProvider
    {
        private string _pointingPokerConnectionString;

        public DbConnectionProvider(string pointingPokerConnectionString)
        {
            _pointingPokerConnectionString = pointingPokerConnectionString;
        }

        public IDbConnection GetOpenPointingPokerConnection()
        {
            var connection = new SqlConnection(_pointingPokerConnectionString);
            connection.Open();
            return connection;
        }
    }
}
