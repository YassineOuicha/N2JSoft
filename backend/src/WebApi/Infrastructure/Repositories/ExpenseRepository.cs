using Microsoft.EntityFrameworkCore;
using WebApi.Application.Interfaces;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Persistence;

namespace WebApi.Infrastructure.Repositories;

internal sealed class ExpenseRepository(AppDbContext db): IExpenseRepository
{
    public async Task<Expense?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await db.Expenses
            .Where(e => !e.IsDeleted)
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<int> CountForUserMonthAsync(Guid userId, int year, int month, CancellationToken ct)
    {
        return await db.Expenses
            .AsNoTracking()
            .Where(e => !e.IsDeleted)
            .Include(e => e.ExpenseReport)
            .Where(e =>
                e.ExpenseReport.UserId == userId &&
                e.ExpenseReport.Year == year &&
                e.ExpenseReport.Month == month
            )
            .CountAsync(ct);
    }

    public async Task AddAsync(Expense expense, CancellationToken ct)
    {
        await db.Expenses.AddAsync(expense, ct);
        await db.SaveChangesAsync(ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await db.SaveChangesAsync(ct);
    }

    public async Task<(IReadOnlyList<Expense> items, int Total)> ListByReportPagedAsync(Guid reportId, int pageNumber, int pageSize, CancellationToken ct)
    {
       var query = db.Expenses
            .AsNoTracking()
            .Where(e => e.ExpenseReportId == reportId)
            .Where((e => !e.IsDeleted))
            .OrderBy(e => e.Date)
            .ThenByDescending(x => x.Id);

        var total = await query.CountAsync(ct);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, total);
    }

    public async Task DeleteAsync(Guid expenseId, CancellationToken ct)
    {
        var expense = await db.Expenses.FirstOrDefaultAsync(e => e.Id == expenseId, ct);
        if (expense != null)
        {
            expense.IsDeleted = true;
            await db.SaveChangesAsync(ct);
        }
    }
}