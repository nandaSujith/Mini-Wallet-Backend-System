using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniWallet.Application.DTOs;
using MiniWallet.Application.Interfaces;

namespace MiniWallet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }



    [HttpGet]
    public async Task<IActionResult> GetHistory(
    [FromQuery] TransactionHistoryRequestDto request)
    {
        var userId = int.Parse(User.FindFirst("UserId")!.Value);

        var result = await _transactionService
            .GetHistoryAsync(userId, request);

        return Ok(result);
    }
}