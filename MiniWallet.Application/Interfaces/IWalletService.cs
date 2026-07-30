using MiniWallet.Application.DTOs;
using MiniWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniWallet.Application.Enums;

namespace MiniWallet.Application.Interfaces
{
    public interface IWalletService
    {
        Task<bool> DepositAsync(int userId, decimal amount);
        Task<Wallet?> GetBalanceAsync(int userId);
        Task<bool> WithdrawAsync(int userId, WithdrawDto dto);
        Task<int> TransferAsync(int senderUserId, TransferDto dto);
    }
}
