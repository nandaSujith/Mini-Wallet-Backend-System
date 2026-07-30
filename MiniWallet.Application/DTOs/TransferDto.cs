namespace MiniWallet.Application.DTOs;

public class TransferDto
{
    public int ToWalletId { get; set; }

    public decimal Amount { get; set; }

    public string ReferenceId { get; set; } = string.Empty;
}