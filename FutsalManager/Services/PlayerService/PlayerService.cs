using Microsoft.EntityFrameworkCore;
using X.PagedList;

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
            return await _context.Players
                .Include(t=>t.Team)
                .Include(a=>a.Attribute)
                .Include(p=>p.Position)
                .ToListAsync();
        }
        public async Task<Player?> GetPlayerAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var player = await _context.Players
                .Include(t=>t.Team)
                .ThenInclude(p=>p.Players)
                .ThenInclude(a=>a.Attribute)
                .Include(t=>t.Team)
                .ThenInclude(p=>p.Players)
                .ThenInclude(a=>a.Position)
                .Include(a=>a.Attribute)
                .Include(p=>p.Position)
                .Include(t=>t.Transfers)
                .ThenInclude(t=>t.Team)
                .FirstOrDefaultAsync(t => t.Id == id);

            return player;
        }
        public async Task<bool> UpdatePlayerAsync(Player player)
        {
            if (player == null)
            {
                return false;
            }

            var model = await _context.Players
                .Include(t=>t.Team)
                .Include(a=>a.Attribute)
                .Include(p=>p.Position)
                .Include(t=>t.Transfers)
                .ThenInclude(t=>t.Team)
                .FirstOrDefaultAsync(t => t.Id == player.Id);
            
            model.FirstName = player.FirstName;
            model.LastName = player.LastName;
            model.Birthday = player.Birthday;
            model.Age = player.Age;
            model.Nationality = player.Nationality;
            
            if (model.Attribute == null)
                model.Attribute = new();
            if (model.Position == null)
                model.Position = new();
            
            model.Attribute.Passing = player.Attribute.Passing;
            model.Attribute.Tackling = player.Attribute.Tackling;
            model.Attribute.Juggling = player.Attribute.Juggling;
            model.Attribute.Dribbling = player.Attribute.Dribbling;
            model.Attribute.Shooting = player.Attribute.Shooting;
            model.Attribute.Reaction = player.Attribute.Reaction;
            model.Attribute.Vision = player.Attribute.Vision;
            model.Attribute.Positioning = player.Attribute.Positioning;

            model.PositionId = player.PositionId;

            _context.Players.Update(model);
            
            return (await _context.SaveChangesAsync()) > 0;
            
        }
        public async Task CreatePlayerAsync(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeletePlayerAsync(int? id)
        {
            var player = await GetPlayerAsync(id);

            if (player == null)
                return false;

            _context.Transfers.RemoveRange(player.Transfers);
            if (player.Attribute != null) _context.Attributes.Remove(player.Attribute);

            _context.Players.Remove(player);

            return (await _context.SaveChangesAsync()) > 0;
        }
        
        public async Task<bool> SetActivityAsync(int? id, bool activity)
        {
            if (id == null) return false;

            var player = await GetPlayerAsync(id);

            if (player == null) return false;
            
            player.IsActive = activity;

            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task<List<Player>> SearchPlayersAsync(string searchstring)
        {
            var result = new List<Player>();
            
            if (string.IsNullOrEmpty(searchstring)) return result;

            result = await _context.Players.Where(p => p.FirstName.ToLower().Contains(searchstring.ToLower())
                || p.LastName.ToLower().Contains(searchstring.ToLower()))
                .Include(t=>t.Team)
                .Include(a=>a.Attribute)
                .Include(p=>p.Position)
                .Include(t=>t.Transfers)
                .ThenInclude(t=>t.Team)
                .ToListAsync();

            return result;
        }

        public async Task<bool> IsActive(int? id)
        {
            var player = await GetPlayerAsync(id);

            return player == null || player.IsActive;
        }
    }
}