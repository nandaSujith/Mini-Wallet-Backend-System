
    namespace MiniWallet.Application.DTOs;

public class TransactionDto
{
    public int TransactionId { get; set; }

    public int WalletId { get; set; }

    public string Type { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public decimal BalanceBefore { get; set; }

    public decimal BalanceAfter { get; set; }

    public string ReferenceId { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}