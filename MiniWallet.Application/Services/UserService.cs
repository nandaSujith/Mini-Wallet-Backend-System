using MiniWallet.Application.DTOs;
using MiniWallet.Application.Interfaces;
using MiniWallet.Domain.Entities;

namespace MiniWallet.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public UserService(
    IUnitOfWork unitOfWork,
    IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }
    public async Task<UserDto> RegisterAsync(RegisterUserDto request)
    {
        var userExists = await _unitOfWork.Users.GetByEmailAsync(request.Email);
        if (userExists != null)
            throw new Exception("Email already exists");

        var phoneExists = await _unitOfWork.Users.GetByPhoneAsync(request.Phone);
        if (phoneExists != null)
            throw new Exception("Mobile number already exists");

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            MobileNumber = request.Phone,
            PasswordHash = request.Password
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var wallet = new Wallet
        {
            UserId = user.UserId,
            Balance = request.InitialBalance
        };

        await _unitOfWork.Wallets.AddAsync(wallet);
        await _unitOfWork.SaveChangesAsync();

        return new UserDto
        {
            Id = user.UserId,
            Name = user.Name,
            Email = user.Email,
            Phone = user.MobileNumber
            
        };
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginDto request)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);

        if (user == null)
            return null;

        if (user.PasswordHash != request.Password)
            return null;


        var token = _jwtService.GenerateToken(
            user.UserId,
            user.Email
        );


        return new LoginResponseDto
        {
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
            Token = token
        };
    }
}