using Microsoft.EntityFrameworkCore;
using MiniWallet.Domain.Entities;

namespace MiniWallet.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Wallet> Wallets => Set<Wallet>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(x => x.UserId);
            entity.HasIndex(x => x.Email).IsUnique();
            entity.HasIndex(x => x.MobileNumber).IsUnique();
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(150).IsRequired();
            entity.Property(x => x.MobileNumber).HasMaxLength(20).IsRequired();
            entity.Property(x => x.PasswordHash).HasMaxLength(255).IsRequired();
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(x => x.WalletId);
            entity.Property(x => x.Balance).HasColumnType("decimal(18,2)");
            entity.Property(x => x.RowVersion).IsRowVersion();
            entity.HasOne(x => x.User)
                  .WithOne(x => x.Wallet)
                  .HasForeignKey<Wallet>(x => x.UserId);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(x => x.TransactionId);
            entity.Property(x => x.Amount).HasColumnType("decimal(18,2)");
            entity.Property(x => x.BalanceBefore).HasColumnType("decimal(18,2)");
            entity.Property(x => x.BalanceAfter).HasColumnType("decimal(18,2)");
            entity.HasIndex(x => x.ReferenceId).IsUnique();
            entity.HasOne(x => x.Wallet)
                  .WithMany(x => x.Transactions)
                  .HasForeignKey(x => x.WalletId);
        });
    }
}