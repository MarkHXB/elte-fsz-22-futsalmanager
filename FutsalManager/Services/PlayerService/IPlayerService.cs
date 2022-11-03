namespace FutsalManager.Services.PlayerService
{
    public interface IPlayerService
    {
        Task<List<Player>> GetPlayersAsync();
        Task<Player?> GetPlayerAsync(int? id);
        Task CreatePlayerAsync(Player player);
        Task<bool> UpdatePlayerAsync(Player player);
        Task<bool> DeletePlayerAsync(int id);
    }
}
