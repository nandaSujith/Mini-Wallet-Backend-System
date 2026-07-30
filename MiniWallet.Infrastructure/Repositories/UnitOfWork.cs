using MiniWallet.Application.Interfaces;          
using MiniWallet.Infrastructure.Persistence;

namespace MiniWallet.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IUserRepository Users { get; }
    public IWalletRepository Wallets { get; }
    public ITransactionRepository Transactions { get; }

    public UnitOfWork(
        ApplicationDbContext context,
        IUserRepository users,
        IWalletRepository wallets,
        ITransactionRepository transactions)
    {
        _context = context;
        Users = users;
        Wallets = wallets;
        Transactions = transactions;
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();
}