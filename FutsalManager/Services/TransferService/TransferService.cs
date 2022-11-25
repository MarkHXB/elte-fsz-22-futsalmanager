using Microsoft.EntityFrameworkCore;

namespace FutsalManager.Services.TransferService
{
    public class TransferService : ITransferService
    {
        private readonly DataContext _context;
        public TransferService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveTransferAsync(Transfer transfer)
        {
            _context.Transfers.Add(transfer);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> CreateTransferForNewbies(Player? player)
        {
            if (player == null) return false;

            if (player.IsActive) return false;
            
            var team = _context.Teams.FirstOrDefault(t => t.Id == player.TeamId);
            player.Team = team;

            if (player?.Team == null) return false;
            
            _context.Players.Add(player);
            
            var transfer = new Transfer()
            {
                Player = player,
                Team = player.Team,
                History = DateTime.Now
            };

            SetActiveStatusPlayer(player);
            
            return (await SaveTransferAsync(transfer));
        }
        
        public async Task<bool> CreateTransferAsync(Player? player, Team? team)
        {
            if (player == null || team == null) return false;

            if (!PlayerExists(player) || !TeamExists(team)) return false;

            if (IsLastTransferWasSameTeam(player, team)) return false;

            if (IsPlayerInAlreadyInSameTeam(player, team)) return false;

            if (player.IsActive) return false;
            
            var transfer = new Transfer()
            {
                Player = player,
                Team = team,
                History = DateTime.Now
            };

            player.Team = team;
            team.Players.Add(player);
            
            SetActiveStatusPlayer(player);
            // SetPlayerTransaction(player, team,transfer);
            // SetTeamTransaction(team, player, transfer);
            
            return (await SaveTransferAsync(transfer));
        }

        public async Task<Transfer?> GetTransferAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var transfer = await _context.Transfers
                .Include(p => p.Player)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(t => t.Id == id);

            return transfer;

        }

        public async Task<List<Transfer>> GetTransfersAsync()
        {
            return await _context.Transfers
                .Include(p => p.Player)
                .Include(t => t.Team)
                .ToListAsync();
        }

        private bool PlayerExists(Player player) => _context.Players.Any(p => p.Id == player.Id);
        private bool TeamExists(Team team) => _context.Teams.Any(p => p.Id == team.Id);
        private bool IsLastTransferWasSameTeam(Player player, Team team)
        {
            //lehet az adatbázisból kiszedve jobb
            if (!player.Transfers.Any()) return false;
            if (player.Transfers.Last().Team.Name == team.Name) return true;
            return false;
        }
        private bool IsPlayerInAlreadyInSameTeam(Player player, Team team)
        {
            var mTeam = _context.Teams.FirstOrDefault(t => t.Id == team.Id);
            var mPlayer = _context.Players.FirstOrDefault(p => p.Id == player.Id);
            if (mTeam == null || mPlayer == null) return false;
            {
                if (mTeam.Players.Any(p => p.Id == mPlayer.Id)) return true;
            }
            return false;
        }
        private void SetActiveStatusPlayer(Player player)
        {
            var model = _context.Players.FirstOrDefault(p => p.Id == player.Id);
            if (model != null) model.IsActive = true; 
        }
    }
}
