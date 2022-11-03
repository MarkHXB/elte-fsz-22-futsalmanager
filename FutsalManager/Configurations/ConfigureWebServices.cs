using FutsalManager.Services.PlayerService;
using FutsalManager.Services.TeamService;
using FutsalManager.Services.TransferService;

namespace FutsalManager.Server.Configurations
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITransferService, TransferService>();

            return services;
        }
    }
}
