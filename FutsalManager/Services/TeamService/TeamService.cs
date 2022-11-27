using Microsoft.EntityFrameworkCore;

namespace FutsalManager.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly DataContext _context;

        public TeamService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Team>> GetTeamsAsync()
        {
            return await _context.Teams
                .Include(p=>p.Players)
                .ToListAsync();
        }
        public async Task<Team?> GetTeamAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var team = await _context.Teams
                .Include(t=>t.Transfers)
                .ThenInclude(t=>t.Player)
                .Include(p=>p.Players)
                .ThenInclude(p=>p.Attribute)
                .Include(p=>p.Players)
                .ThenInclude(p=>p.Position)
                .FirstOrDefaultAsync(t => t.Id == id);

            return team;
        }
        public async Task<bool> UpdateTeamAsync(Team team)
        {
            _context.Entry(await _context.Teams.FirstOrDefaultAsync(x => x.Id == team.Id)).CurrentValues.SetValues(team);
            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task CreateTeamAsync(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteTeamAsync(int id)
        {
            var team = await GetTeamAsync(id);

            if (team == null) return false;

            _context.Transfers.RemoveRange(team.Transfers);
            _context.Teams.Remove(team);

            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task<List<Team>> SearchTeamsAsync(string searchstring)
        {
            var result = new List<Team>();
            
            if (string.IsNullOrEmpty(searchstring)) return result;

            result = await _context.Teams.Where(p => p.Name.ToLower().Contains(searchstring.ToLower()))
                .Include(p=>p.Players)
                .ToListAsync();

            return result;
        }
    }
}
