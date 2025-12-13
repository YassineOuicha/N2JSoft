using WebApi.Application.Dtos;
using WebApi.Domain.Entities;

namespace WebApi.Application.Interfaces;

public interface IExpenseReportRepository
{
    Task<ExpenseReport?> GetByIdAsync(Guid expenseReportId, CancellationToken ct);
    Task<bool> ExistsForUserMonthAsync(Guid userId, int year, int month, CancellationToken ct);
    Task AddAsync(ExpenseReport expenseReport, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
    Task DeleteAsync(ExpenseReport expenseReport, CancellationToken ct);
    Task<IReadOnlyList<ExpenseReport>> ListAsync(CancellationToken ct);
}