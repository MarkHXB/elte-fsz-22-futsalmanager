using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace FutsalManager.Services.PositionService
{
    public class PositionService : IPositionService
    {
        private readonly DataContext _context;

        public PositionService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Position>> GetPositionsAsync()
        {
            return  await _context.Positions.ToListAsync();
        }
    }
}