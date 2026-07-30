using MiniWallet.Domain.Entities;
using MiniWallet.Domain.Enums;

public class Transaction
{
    public int TransactionId { get; set; }

    public int WalletId { get; set; }

    public decimal Amount { get; set; }

    public decimal BalanceBefore { get; set; }

    public decimal BalanceAfter { get; set; }

    public string ReferenceId { get; set; } = Guid.NewGuid().ToString();

    public string Type { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Completed";

    public Wallet Wallet { get; set; } = null!;
}