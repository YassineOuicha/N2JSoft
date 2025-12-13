using WebApi.Application.Dtos;
using WebApi.Application.Interfaces;
using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Application.UseCases;

internal sealed class ExpenseService(
    IUserRepository users,
    IExpenseReportRepository reports,
    IExpenseRepository expenses)
{
    public async Task<Guid?> CreateAsync(Guid reportId, CreateExpenseDto dto, CancellationToken ct)
    {
        var report = await reports.GetByIdAsync(reportId, ct);
        if (report == null)
        {
            return null;
        }
        
        var user = await users.GetByIdAsync(report.UserId, ct);
        if (user == null || user.IsDeleted || !user.IsActive)
        {
            return null;
        }

        if (dto.Description.Length > 50)
        {
            // to do : errors handling
            return null; // Description is too long
        }
        
        var count = await expenses.CountForUserMonthAsync(user.Id, report.Year, report.Month, ct);
        
        if (count >= user.MonthlyExpenseLimit){
            // to do : errors handling
            return null; // Monthly limit reached, no more expenses allowed
        }


        var expense = new Expense
        {
            Id = Guid.NewGuid(),
            ExpenseReportId = report.Id,
            Date = dto.Date,
            Description = dto.Description,
            Amount = dto.Amount,
            BillingAddress = new BillingAddress(dto.Brand, dto.Street, dto.City, dto.City),
            IsDeleted = false,
        };
        
        await expenses.AddAsync(expense, ct);
        await expenses.SaveChangesAsync(ct);
        
        return expense.Id;
    }
    
    public async Task<bool> UpdateAsync(Guid expenseId, UpdateExpenseDto dto, CancellationToken ct)
    {
        var expense = await expenses.GetByIdAsync(expenseId, ct);
        if (expense == null || expense.IsDeleted)
        {
            return false;
        }

        if (dto.Description.Length > 50)
        {
            // to do : errors handling
            return false; // Description is too long
        }
        
        expense.Date = dto.Date;
        expense.Description = dto.Description;
        expense.Amount = dto.Amount;
        expense.BillingAddress = new BillingAddress(dto.Brand, dto.Street, dto.PostalCode, dto.City);
        
        await expenses.SaveChangesAsync(ct);
        
        return true;
    }

    public async Task<PagedResultDto<ExpenseListItemDto>?> ListByReportPagedAsync(Guid reportId, int pageNumber, int pageSize, CancellationToken ct)
    {
        var report = await reports.GetByIdAsync(reportId, ct);
        if (report == null)
        {
            return null;
        }

        var (items, total) = await expenses.ListByReportPagedAsync(reportId, pageNumber, pageSize, ct);
        var dtos = items.Select(e => new ExpenseListItemDto(
            e.Id,
            e.Date,
            e.Description,
            e.Amount,
            e.BillingAddress.Brand,
            e.BillingAddress.Street, 
            e.BillingAddress.City,
            e.BillingAddress.PostalCode
        )).ToList();
        
        return new PagedResultDto<ExpenseListItemDto>(dtos, total, pageNumber, pageSize);
    }
}