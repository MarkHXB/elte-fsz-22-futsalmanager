namespace FutsalManager.Services.TeamService
{
    public interface ITeamService
    {
        Task<List<Team>> GetTeamsAsync();
        Task<Team?> GetTeamAsync(int? id);
        Task CreateTeamAsync(Team team);
        Task<bool> UpdateTeamAsync(Team team);
        Task<bool> DeleteTeamAsync(int id);
    }
}
