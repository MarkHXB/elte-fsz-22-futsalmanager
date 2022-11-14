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

        public async Task<bool> CreateTransferAsync(Player? player, Team? team)
        {
            if (player == null || team == null) return false;

            if (!PlayerExists(player) || !TeamExists(team)) return false;

            if (IsLastTransferWasSameTeam(player, team)) return false;

            var transfer = new Transfer()
            {
                Player = player,
                Team = team,
                History = DateTime.Now
            };

            SetActiveStatusPlayer(player);
            SetPlayerTransaction(player, team);
            SetTeamTransaction(team, player);

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
            if (player.Transfers.Last().Team.Name == team.Name) return false;
            return true;
        }

        private void SetActiveStatusPlayer(Player player)
        {
            var model = _context.Players.FirstOrDefault(p => p.Id == player.Id);
            if (model != null) model.IsActive = true; 
        }
        
        private void SetPlayerTransaction(Player player, Team team)
        {
            var model = _context.Players.FirstOrDefault(p => p.Id == player.Id);
            if (model != null)
                model.Team = team;
        }
        
        private void SetTeamTransaction(Team team,Player player)
        {
            var model = _context.Teams.FirstOrDefault(p => p.Id == team.Id);
            model?.Players.Add(player);
        }
    }
}
