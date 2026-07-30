using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniWallet.Application.DTOs;
using MiniWallet.Application.Interfaces;
using MiniWallet.Application.Services;

namespace MiniWallet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WalletsController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletsController(IWalletService walletService)
    {
        _walletService = walletService;
    }

    [HttpGet("balance")]
    public async Task<IActionResult> GetBalance()
    {
        var userId = int.Parse(User.FindFirst("UserId")!.Value);

        var wallet = await _walletService.GetBalanceAsync(userId);

        if (wallet == null)
            return NotFound();

        return Ok(new
        {
            balance = wallet.Balance
        });
    }
    [HttpPost("CreditWallet")]
    public async Task<IActionResult> Deposit(DepositDto dto)
    {
        var userId = int.Parse(User.FindFirst("UserId")!.Value);

        var result = await _walletService
            .DepositAsync(userId, dto.Amount);

        if (!result)
            return BadRequest("Wallet not found");

        return Ok(new
        {
            message = "Deposit successful"
        });
    }
    [HttpPost("Debit Wallet")]
    public async Task<IActionResult> Withdraw(WithdrawDto dto)
    {
        var userId = int.Parse(User.FindFirst("UserId")!.Value);

        var result = await _walletService
            .WithdrawAsync(userId, dto);

        if (!result)
        {
            return BadRequest(new
            {
                message = "Insufficient balance or wallet not found"
            });
        }

        return Ok(new
        {
            message = "Withdrawal successful"
        });
    }
    [HttpPost("Wallet_to_Wallet_transfer")]
    public async Task<IActionResult> Transfer(TransferDto dto)
    {
        var userId = int.Parse(User.FindFirst("UserId")!.Value);
        //var userId = 2;
        var result = await _walletService
            .TransferAsync(userId, dto);
        var mes = "null";

        //if (!result)
        //{
        //    return BadRequest(new
        //    {
        //        message = "Transfer failed. Check wallet, balance, or reference ID."
        //    });
        //}
        switch(result)
        {
            case 1:
                mes = "insufficient Balance";
                break;
            case 2:
                mes = "Receiver wallet not found";
                break;
            case 3:
                mes = "sender and receiver Wallet addresses cannot be same";
                break;
            case 4:
                mes = "Negative wallet balance should not be allowed.";
                break;
            case 5:
                return Ok(new
                {
                    message = "Transfer successful"
                });
                //break;
            default:
                    return BadRequest(new
                    {
                        message = "Transfer failed. Check wallet, balance, or reference ID."
                    });
                
        }
       
            return BadRequest(new
            {
                message = mes
            });

       
    }
}