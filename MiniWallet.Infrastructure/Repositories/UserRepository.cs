using Microsoft.EntityFrameworkCore;
using MiniWallet.Application.Interfaces;          // ← correct
using MiniWallet.Domain.Entities;
using MiniWallet.Infrastructure.Persistence;

namespace MiniWallet.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

    public async Task<User?> GetByPhoneAsync(string phone)
        => await _context.Users.FirstOrDefaultAsync(x => x.MobileNumber == phone);
}