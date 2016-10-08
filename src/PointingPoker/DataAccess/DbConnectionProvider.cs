using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PointingPoker.DataAccess
{
    public class DbConnectionProvider : IDbConnectionProvider
    {
        private string _pointingPokerConnectionString;

        public DbConnectionProvider(IConfigurationRoot configuration)
        {
            _pointingPokerConnectionString = configuration
                .GetConnectionString("PointingPoker");
        }

        public IDbConnection GetOpenPointingPokerConnection()
        {
            var connection = new SqlConnection(_pointingPokerConnectionString);
            connection.Open();
            return connection;
        }
    }
}
