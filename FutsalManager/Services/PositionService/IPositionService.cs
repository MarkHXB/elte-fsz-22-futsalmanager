namespace FutsalManager.Services.PositionService
{
    public interface IPositionService
    {
        Task<List<Position>> GetPositionsAsync();
    }
}
