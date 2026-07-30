using MiniWallet.Application.DTOs;


namespace MiniWallet.Application.Interfaces;

public interface ITransactionService
{
   
    Task<List<TransactionDto>> GetHistoryAsync(
    int userId,
    TransactionHistoryRequestDto request);

}