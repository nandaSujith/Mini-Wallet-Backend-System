using Microsoft.Extensions.DependencyInjection;
using MiniWallet.Application.Interfaces;
using MiniWallet.Application.Services;
using FluentValidation;

namespace MiniWallet.Application
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IWalletService, WalletService>();

            services.AddScoped<ITransactionService, TransactionService>();
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);


            return services;
        }
    }
}