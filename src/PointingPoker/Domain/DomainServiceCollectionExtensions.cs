using Microsoft.Extensions.DependencyInjection;

namespace PointingPoker.Domain
{
    static class DomainServiceCollectionExtensions
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ICardService, CardService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
