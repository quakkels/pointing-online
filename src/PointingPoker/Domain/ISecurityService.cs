using System;
using System.Threading.Tasks;

namespace PointingPoker.Domain
{
    public interface ISecurityService
    {
        Task SignIn(Guid userId);
        Task SignOut();
        bool VerifyUserPassword(string username, string password);
    }
}
