namespace MiniWallet.Application.Enums;

public enum TransferResult
{
    Failed = 0,

    InsufficientBalance = 1,

    ReceiverNotFound = 2,

    SameWallet = 3,

    NegativeBalance = 4,

    Success = 5
}