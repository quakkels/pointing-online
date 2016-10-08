using System.Data;

namespace PointingPoker.DataAccess
{
    public interface IDbConnectionProvider
    {
        IDbConnection GetOpenPointingPokerConnection();
    }
}
