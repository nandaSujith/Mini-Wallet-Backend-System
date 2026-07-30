using Microsoft.EntityFrameworkCore;
using MiniWallet.Application.Interfaces;          
using MiniWallet.Domain.Entities;
using MiniWallet.Infrastructure.Persistence;

namespace MiniWallet.Infrastructure.Repositories;

public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
{
    private readonly ApplicationDbContext _context;

    public WalletRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

   
    public async Task<Wallet?> GetByUserIdAsync(int userId)
    {
        return await _context.Wallets
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }
   
}