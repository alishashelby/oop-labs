using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Transaction;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using AdminService = Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Services.AdminService;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IAccountService, AccountService>();
        collection.AddScoped<IAdminService, AdminService>();
        collection.AddScoped<ITransactionService, TransactionService>();

        collection.AddScoped<CurrentAccountService>();
        collection.AddScoped<ICurrentAccountService>(
            p => p.GetRequiredService<CurrentAccountService>());

        return collection;
    }
}