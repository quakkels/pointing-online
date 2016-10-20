using Microsoft.Extensions.DependencyInjection;

namespace PointingPoker.Domain
{
    static class DomainServiceCollectionExtensions
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<ICardService, CardService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<SignedInUserService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IPointService, PointService>();
        }
    }
}
