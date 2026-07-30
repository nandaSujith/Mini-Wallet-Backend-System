using MiniWallet.Application.DTOs;
using MiniWallet.Domain.Entities;

namespace MiniWallet.Application.Interfaces;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);

    Task<List<Transaction>> GetByWalletIdAsync(int walletId);
    Task<bool> ReferenceExistsAsync(string referenceId);
    Task<List<Transaction>> GetHistoryAsync(
    int walletId,
    TransactionHistoryRequestDto request);
}