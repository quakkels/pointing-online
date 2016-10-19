using Microsoft.Extensions.DependencyInjection;
using PointingPoker.DataAccess.Commands;
using PointingPoker.DataAccess.Queries;

namespace PointingPoker.DataAccess
{
    static class DataAccessServiceCollectionExtensions
    {
        public static void AddDataAccess(
            this IServiceCollection services, 
            string pointingPokerConnectionString)
        {
            services.AddSingleton<IDbConnectionProvider>(
                new DbConnectionProvider(pointingPokerConnectionString));
            services.AddTransient<ICardCommands, CardCommands>();
            services.AddTransient<ICardQueries, CardQueries>();
            services.AddTransient<IUserQueries, UserQueries>();
            services.AddTransient<IUserCommands, UserCommands>();
            services.AddTransient<ITeamCommands, TeamCommands>();
            services.AddTransient<ITeamQueries, TeamQueries>();
        }
    }
}
