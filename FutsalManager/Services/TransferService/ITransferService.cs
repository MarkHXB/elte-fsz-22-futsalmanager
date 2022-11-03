namespace FutsalManager.Services.TransferService
{
    public interface ITransferService
    {
        Task CreateTransferAsync(Transfer transfer);
        Task<List<Transfer>> GetTransfersAsync();
        Task<Transfer?> GetTransferAsync(int? id);
    }
}
