using MiniWallet.Application.DTOs;

namespace MiniWallet.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> RegisterAsync(RegisterUserDto request);

    Task<LoginResponseDto?> LoginAsync(LoginDto request);
}