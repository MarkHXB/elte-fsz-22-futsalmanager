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

        public async Task CreateTransferAsync(Transfer transfer)
        {
            _context.Transfers.Add(transfer);
            await _context.SaveChangesAsync();
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
    }
}
