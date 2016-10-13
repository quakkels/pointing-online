using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PointingPoker.Domain
{
    public class SecurityService : ISecurityService
    {
        private HttpContext _httpContext;
        public SecurityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
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

        public bool VerifyUserPassword(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
