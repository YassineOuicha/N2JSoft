using WebApi.Domain.Entities;

namespace WebApi.Application.Interfaces;

public interface IExpenseRepository
{
    Task<Expense?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<int> CountForUserMonthAsync(Guid userId, int year, int month, CancellationToken ct);
    Task AddAsync(Expense expense, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
    Task<(IReadOnlyList<Expense> items, int Total)> ListByReportPagedAsync(
        Guid reportId,
        int pageNumber,
        int pageSize,
        CancellationToken ct);

    Task DeleteAsync(Guid expenseId, CancellationToken ct);
}