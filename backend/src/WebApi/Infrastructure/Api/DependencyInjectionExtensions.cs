using WebApi.Application.Interfaces;
using WebApi.Application.UseCases;
using WebApi.Infrastructure.Repositories;

namespace WebApi.Infrastructure.Api;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IExpenseReportRepository, ExpenseReportRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();

        services.AddScoped<UserService>();
        services.AddScoped<ExpenseReportService>();
        services.AddScoped<ExpenseService>();
        
        return services;
    }
}