using Microsoft.EntityFrameworkCore;

namespace FutsalManager.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly DataContext _context;

        public PlayerService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Player>> GetPlayersAsync()
        {
            return await _context.Players.ToListAsync();
        }
        public async Task<Player?> GetPlayerAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var player = await _context.Players
                .Include(t=>t.Team)
                .Include(a=>a.Attribute)
                .Include(p=>p.Position)
                .Include(t=>t.Transfers)
                .FirstOrDefaultAsync(t => t.Id == id);

            return player;

        }
        public async Task<bool> UpdatePlayerAsync(Player player)
        {
            _context.Entry(await _context.Players.FirstOrDefaultAsync(x => x.Id == player.Id)).CurrentValues.SetValues(player);
            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task CreatePlayerAsync(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeletePlayerAsync(int id)
        {
            var player = await GetPlayerAsync(id);

            if (player == null)
                return false;

            _context.Players.Remove(player);

            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}