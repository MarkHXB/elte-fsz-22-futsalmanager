namespace FutsalManager.Services.PlayerService
{
    public interface IPlayerService
    {
        Task<List<Player>> GetPlayersAsync();
        Task<Player?> GetPlayerAsync(int? id);
        Task CreatePlayerAsync(Player player);
        Task<bool> UpdatePlayerAsync(Player player);
        Task<bool> DeletePlayerAsync(int? id);
        Task<bool> SetActivityAsync(int? id, bool activity);
        Task<List<Player>> SearchPlayersAsync(string searchstring);
        Task<bool> IsActive(int? id);
    }
}
