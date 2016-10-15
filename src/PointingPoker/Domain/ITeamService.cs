using PointingPoker.DataAccess.Models;

namespace PointingPoker.Domain
{
    public interface ITeamService
    {
        bool CreateTeam(Team team);
    }
}
