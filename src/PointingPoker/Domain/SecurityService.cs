using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using PointingPoker.DataAccess.Queries;

namespace PointingPoker.Domain
{
    public class SecurityService : ISecurityService
    {
        private HttpContext _httpContext;
        private IUserQueries _userQueries; 
        public SecurityService(IHttpContextAccessor httpContextAccessor, IUserQueries userQueries)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _userQueries = userQueries;
        }

        public async Task SignIn(Guid userId)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("id", userId.ToString()));

            var identity = new ClaimsIdentity(
                claims, 
                CookieAuthenticationDefaults.AuthenticationScheme);

            await _httpContext.Authentication.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(identity));
        }

        public async Task SignOut()
        {
            await _httpContext.Authentication
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool VerifyUserPassword(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            var retrievedPassword = _userQueries.GetPasswordHashByUserName(userName);

            return password == retrievedPassword;
        }
    }
}
