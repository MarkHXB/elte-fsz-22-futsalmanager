namespace FutsalManager.Services.TransferService
{
    public interface ITransferService
    {
        Task<bool> SaveTransferAsync(Transfer transfer);
        Task<bool> CreateTransferAsync(Player? player, Team? team);
        Task<List<Transfer>> GetTransfersAsync();
        Task<Transfer?> GetTransferAsync(int? id);
    }
}
