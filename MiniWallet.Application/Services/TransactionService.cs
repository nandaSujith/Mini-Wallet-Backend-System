using MiniWallet.Application.DTOs;
using MiniWallet.Application.Interfaces;

namespace MiniWallet.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<List<TransactionDto>> GetHistoryAsync(
    int userId,
    TransactionHistoryRequestDto request)
    {
        var wallet = await _unitOfWork.Wallets
            .GetByUserIdAsync(userId);

        if (wallet == null)
            return new List<TransactionDto>();

        var transactions = await _unitOfWork.Transactions
            .GetHistoryAsync(wallet.WalletId, request);

        return transactions.Select(x => new TransactionDto
        {
            TransactionId = x.TransactionId,
            WalletId = x.WalletId,
            Type = x.Type,
            Amount = x.Amount,
            BalanceBefore = x.BalanceBefore,
            BalanceAfter = x.BalanceAfter,
            ReferenceId = x.ReferenceId,
            Status = x.Status,
            CreatedAt = x.CreatedAt
        }).ToList();
    }
}