namespace MiniWallet.Application.DTOs;

public class WalletDto
{
    public int WalletId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public DateTime LastUpdated { get; set; }
}