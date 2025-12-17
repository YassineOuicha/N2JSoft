using WebApi.Application.Dtos;
using WebApi.Application.Interfaces;
using WebApi.Domain.Entities;
using WebApi.Domain.Errors;
using WebApi.Domain.ValueObjects;

namespace WebApi.Application.UseCases;

public sealed class ExpenseService(
    IUserRepository users,
    IExpenseReportRepository reports,
    IExpenseRepository expenses)
{
    public async Task<(Guid? Id, DomainError? Error)> CreateAsync(Guid reportId, CreateExpenseDto dto, CancellationToken ct)
    {
        var report = await reports.GetByIdAsync(reportId, ct);
        if (report == null)
        {
            return (null, new DomainError("report.notfound", "Expense report not found"));
        }
        
        var user = await users.GetByIdAsync(report.UserId, ct);
        if (user == null || user.IsDeleted )
        {
            return (null, DomainErrors.UserDeleted(report.UserId));
        }
        
        if (!user.IsActive)
        {
            return (null, DomainErrors.UserInactive(report.UserId));
        }
        
        var descError = ExpensePolicy.ValidateDescription(dto.Description);
        if (descError != null)
        { 
            return (null, descError);
        }
        
        var count = await expenses.CountForUserMonthAsync(user.Id, report.Year, report.Month, ct);
        if (count >= user.MonthlyExpenseLimit)
        {
            return (null, DomainErrors.MonthlyQuotaReached(user.MonthlyExpenseLimit));
        }


        var expense = new Expense
        {
            Id = Guid.NewGuid(),
            ExpenseReportId = report.Id,
            Date = dto.Date,
            Description = dto.Description,
            Amount = dto.Amount,
            BillingAddress = new BillingAddress(dto.Brand, dto.Street, dto.PostalCode, dto.City),
            IsDeleted = false,
        };
        
        await expenses.AddAsync(expense, ct);
        await expenses.SaveChangesAsync(ct);
        
        return (expense.Id, null);
    }
    
    public async Task<(bool Ok, DomainError? Error)> UpdateAsync(Guid expenseId, UpdateExpenseDto dto, CancellationToken ct)
    {
        var expense = await expenses.GetByIdAsync(expenseId, ct);
        if (expense == null)
        {
            return (false, new DomainError("expense.notfound", "Expense not found"));
        }

        var descError = ExpensePolicy.ValidateDescription(dto.Description);
        if (descError != null)
        { 
            return (false, descError);
        }
        
        expense.Date = dto.Date;
        expense.Description = dto.Description;
        expense.Amount = dto.Amount;
        expense.BillingAddress = new BillingAddress(dto.Brand, dto.Street, dto.PostalCode, dto.City);
        
        await expenses.SaveChangesAsync(ct);
        
        return (true, null);
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

    public async Task<bool> DeleteAsync(Guid expenseId, CancellationToken ct)
    {
        var expense = await expenses.GetByIdAsync(expenseId, ct);
        if (expense == null)
        {
            return false;
        }

        await expenses.DeleteAsync(expenseId, ct);
        await expenses.SaveChangesAsync(ct);

        return true;
    }
}