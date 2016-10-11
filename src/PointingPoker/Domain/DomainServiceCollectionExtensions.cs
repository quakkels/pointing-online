using Microsoft.Extensions.DependencyInjection;

namespace PointingPoker.Domain
{
    static class DomainServiceCollectionExtensions
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICardService, CardService>();
        }
    }
}
