using Microsoft.EntityFrameworkCore;
using WebApi.Application.Dtos;
using WebApi.Application.Interfaces;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Persistence;

namespace WebApi.Infrastructure.Repositories;

internal sealed class ExpenseReportRepository(AppDbContext db): IExpenseReportRepository
{
    public async Task<ExpenseReport?> GetByIdAsync(Guid expenseReportId, CancellationToken ct)
    {
        return await db.ExpenseReports
            .FirstOrDefaultAsync(er => er.Id == expenseReportId, ct);
    }

    public async Task<bool> ExistsForUserMonthAsync(Guid userId, int year, int month, CancellationToken ct)
    {
        return  await db.ExpenseReports
            .AnyAsync(er => er.UserId == userId && er.Year == year && er.Month == month, ct);
    }

    public async Task AddAsync(ExpenseReport expenseReport, CancellationToken ct)
    {
        await db.ExpenseReports.AddAsync(expenseReport, ct);
        await db.SaveChangesAsync(ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(ExpenseReport expenseReport, CancellationToken ct)
    {
        var report = db.ExpenseReports.FirstOrDefault(er => er.Id == expenseReport.Id);
        if (report is not null)
        {
            db.ExpenseReports.Remove(report);
            await db.SaveChangesAsync(ct);
        }
    }

    public async Task<IReadOnlyList<ExpenseReport>> ListAsync(CancellationToken ct)
    {
        return await db.ExpenseReports.AsNoTracking()
            .AsQueryable()
            .Include(er => er.User)
            .Where(er => !er.User.IsDeleted)
            .OrderByDescending(er => er.Year)
            .ThenByDescending(er => er.Month)
            .ToListAsync(ct);
    }
}