using MiniWallet.Application.Interfaces;

namespace MiniWallet.Application.Interfaces; 

public interface IUnitOfWork
{
    IUserRepository Users { get; }

    IWalletRepository Wallets { get; }

    ITransactionRepository Transactions { get; }

    Task<int> SaveChangesAsync();
}