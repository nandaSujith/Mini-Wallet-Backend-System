using MiniWallet.Domain.Entities;
namespace MiniWallet.Application.Interfaces
{
    public interface IWalletRepository: IGenericRepository<Wallet>
    {
        Task<Wallet?> GetByUserIdAsync(int userId);
        
    }
}
