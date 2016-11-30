using PointingPoker.Domain.Models;
using System;

namespace PointingPoker.Domain
{
    public class SignedInUserService
    {
        private ISecurityService _securityService;

        public SignedInUserService(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public SignedInUser GetSignedInUser()
        {
            return _securityService.GetSignedInUser();
        }

        public bool IsSignedIn()
        {
            var userId = _securityService.GetCurrentUserId();
            return userId != 0;
        }
    }
}
