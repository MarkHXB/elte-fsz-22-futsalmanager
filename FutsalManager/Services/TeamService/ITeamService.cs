﻿namespace FutsalManager.Services.TeamService
{
    public interface ITeamService
    {
        Task<List<Team>> GetTeamsAsync();
        Task<Team?> GetTeamAsync(int? id);
        Task CreateTeamAsync(Team team);
        Task<bool> UpdateTeamAsync(Team team);
        Task<bool> DeleteTeamAsync(int id);
        Task<List<Team>> SearchTeamsAsync(string searchstring);
        bool CheckCapacity();
        Task<bool> CheckFreeSpaceInTeam(int? teamId);
    }
}
