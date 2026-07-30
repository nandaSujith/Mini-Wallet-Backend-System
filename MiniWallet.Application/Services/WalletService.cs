using MiniWallet.Application.Interfaces;
using MiniWallet.Domain.Entities;
using MiniWallet.Domain.Enums;
using Microsoft.Extensions.Logging;
using MiniWallet.Application.DTOs;
using MiniWallet.Application.Enums;

namespace MiniWallet.Application.Services;


public class WalletService : IWalletService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<WalletService> _logger;

    public WalletService(IUnitOfWork unitOfWork, ILogger<WalletService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<bool> DepositAsync(int userId, decimal amount)
    {
        _logger.LogInformation("Deposit started for UserId {UserId}, Amount {Amount}", userId, amount);
        var wallet = await _unitOfWork.Wallets
            .GetByUserIdAsync(userId);

        if (wallet == null)
            return false;


        var balanceBefore = wallet.Balance;

        wallet.Balance += amount;
        wallet.LastUpdated = DateTime.UtcNow;


        var transaction = new Transaction
        {
            WalletId = wallet.WalletId,
            Amount = amount,
            BalanceBefore = balanceBefore,
            BalanceAfter = wallet.Balance,
            ReferenceId = Guid.NewGuid().ToString(),
            Type = "Credit",
            Status = "Completed"
        };


        await _unitOfWork.Transactions.AddAsync(transaction);

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Deposit completed for WalletId {WalletId}, New Balance {Balance}", wallet.WalletId, wallet.Balance);

        return true;
    }
    public async Task<Wallet?> GetBalanceAsync(int userId)
    {
        return await _unitOfWork.Wallets
            .GetByUserIdAsync(userId);
    }
    public async Task<bool> WithdrawAsync(int userId, WithdrawDto dto)
    {
        _logger.LogInformation(
            "Withdraw started for UserId {UserId}, Amount {Amount}",
            userId,
            dto.Amount);

        var wallet = await _unitOfWork.Wallets
            .GetByUserIdAsync(userId);

        if (wallet == null)
            return false;


        if (wallet.Balance < dto.Amount)
        {
            _logger.LogWarning(
                "Insufficient balance for UserId {UserId}",
                userId);

            return false;
        }


        if (await _unitOfWork.Transactions
            .ReferenceExistsAsync(dto.ReferenceId))
        {
            _logger.LogWarning(
                "Duplicate reference id {ReferenceId}",
                dto.ReferenceId);

            return false;
        }


        var balanceBefore = wallet.Balance;

        wallet.Balance -= dto.Amount;

        wallet.LastUpdated = DateTime.UtcNow;


        var transaction = new Transaction
        {
            WalletId = wallet.WalletId,

            Amount = dto.Amount,

            BalanceBefore = balanceBefore,

            BalanceAfter = wallet.Balance,

            ReferenceId = dto.ReferenceId,

            Type = "Debit",

            Status = "Completed",

            CreatedAt = DateTime.UtcNow
        };


        await _unitOfWork.Transactions.AddAsync(transaction);

        await _unitOfWork.SaveChangesAsync();


        _logger.LogInformation(
            "Withdraw completed for UserId {UserId}",
            userId);

        return true;
    }
    public async Task<int> TransferAsync(int fromUserId, TransferDto dto)
    {
        var senderWallet = await _unitOfWork.Wallets
            .GetByUserIdAsync(fromUserId);

        if (senderWallet == null)
            return 1;


        var receiverWallet = await _unitOfWork.Wallets
            .GetByIdAsync(dto.ToWalletId);


        if (receiverWallet == null)
            return 2;


        if (senderWallet.WalletId == receiverWallet.WalletId)
            return 3;


        if (senderWallet.Balance < dto.Amount)
            return 4;




        var senderBalanceBefore = senderWallet.Balance;
        var receiverBalanceBefore = receiverWallet.Balance;


        senderWallet.Balance -= dto.Amount;
        receiverWallet.Balance += dto.Amount;


        var debitTransaction = new Transaction
        {
            WalletId = senderWallet.WalletId,
            Type = "Debit",
            Amount = dto.Amount,
            BalanceBefore = senderBalanceBefore,
            BalanceAfter = senderWallet.Balance,
            Status = "Completed",
            CreatedAt = DateTime.UtcNow
        };


        var creditTransaction = new Transaction
        {
            WalletId = receiverWallet.WalletId,
            Type = "Credit",
            Amount = dto.Amount,
            BalanceBefore = receiverBalanceBefore,
            BalanceAfter = receiverWallet.Balance,
            Status = "Completed",
            CreatedAt = DateTime.UtcNow
        };


        await _unitOfWork.Transactions.AddAsync(debitTransaction);

        await _unitOfWork.Transactions.AddAsync(creditTransaction);


        await _unitOfWork.SaveChangesAsync();


        return 5;
    }
}