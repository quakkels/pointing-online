using PointingPoker.Domain.Models;
using System;
using System.Threading.Tasks;

namespace PointingPoker.Domain
{
    public interface ISecurityService
    {
        Task SignIn(int userId);
        Task SignOut();
        bool VerifyUserPassword(string username, string password);
        int GetCurrentUserId();
        SignedInUser GetSignedInUser();
    }
}
