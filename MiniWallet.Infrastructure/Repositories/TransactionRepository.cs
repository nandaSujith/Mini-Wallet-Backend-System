using Microsoft.EntityFrameworkCore;
using MiniWallet.Application.DTOs;
using MiniWallet.Application.Interfaces;
using MiniWallet.Domain.Entities;
using MiniWallet.Infrastructure.Persistence;

namespace MiniWallet.Infrastructure.Repositories;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }


    public async Task<List<Transaction>> GetByWalletIdAsync(int walletId)
    {
        return await _context.Transactions
            .Where(x => x.WalletId == walletId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
    public async Task<bool> ReferenceExistsAsync(string referenceId)
    {
        return await _context.Transactions
            .AnyAsync(x => x.ReferenceId == referenceId);
    }
    public async Task<List<Transaction>> GetHistoryAsync(
    int walletId,
    TransactionHistoryRequestDto request)
    {
        var query = _context.Transactions
            .Where(x => x.WalletId == walletId);

        if (!string.IsNullOrWhiteSpace(request.Type))
        {
            query = query.Where(x => x.Type == request.Type);
        }

        if (request.FromDate.HasValue)
        {
            query = query.Where(x => x.CreatedAt >= request.FromDate.Value.Date);
        }

        if (request.ToDate.HasValue)
        {
            var endDate = request.ToDate.Value.Date.AddDays(1);

            query = query.Where(x => x.CreatedAt < endDate);
        }

        return await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
    }
}