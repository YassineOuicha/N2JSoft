using System.Globalization;
using WebApi.Application.Dtos;
using WebApi.Application.Interfaces;
using WebApi.Domain.Entities;
using WebApi.Domain.Errors;

namespace WebApi.Application.UseCases;

public sealed class ExpenseReportService(IUserRepository users, IExpenseReportRepository reports)
{
    public async Task<(Guid? Id, DomainError? Error)> CreateAsync(CreateExpenseReportDto dto, CancellationToken ct)
    {
       var user = await users.GetByIdAsync(dto.UserId, ct);
       if (user == null || user.IsDeleted)
       {
           return (null, DomainErrors.UserDeleted(dto.UserId));
       }

       if (!user.IsActive)
       {
           return (null, DomainErrors.UserInactive(dto.UserId));
       }
       
       var exists = await reports.ExistsForUserMonthAsync(dto.UserId, dto.Year, dto.Month, ct); 
       if (exists) 
       { 
           return (null, new DomainError("report.duplicate", "An expense already exists for this user/month."));
       }
       
       var title = BuildTitle(user.FirstName, user.LastName, dto.Month, dto.Year);

       var report = new ExpenseReport
       {
           Id = Guid.NewGuid(),
           UserId = dto.UserId,
           Month = dto.Month,
           Year = dto.Year,
           Title = title
       };
       
       await reports.AddAsync(report, ct);
       await reports.SaveChangesAsync(ct);
       
       return (report.Id, null);
    }
    
    private static string BuildTitle(string firstName, string lastName, int month, int year)
    {
        var dt = new DateTime(year, month, 1);
        var monthName = dt.ToString("MMMM", CultureInfo.GetCultureInfo("fr-FR"));
        monthName = CultureInfo.GetCultureInfo("fr-FR").TextInfo.ToTitleCase(monthName);
        return $"{firstName} {lastName} - {monthName} {year}";
    }
    
    public async Task<bool> DeleteAsync(Guid reportId, CancellationToken ct)
    {
        var report = await reports.GetByIdAsync(reportId, ct);
        if (report == null)
        {
            return false;
        }
        
        await reports.DeleteAsync(report, ct);
        await reports.SaveChangesAsync(ct);
        
        return true;
    }

    public async Task<IReadOnlyList<ExpenseReportListItemDto>> ListAsync(CancellationToken ct)
    { 
        var expenseReports = await reports.ListAsync(ct);
        return expenseReports
            .Select(er => new ExpenseReportListItemDto(
                    er.Id,
                    er.UserId,
                    $"{er.User.FirstName} {er.User.LastName}",
                    er.Year,
                    er.Month,
                    er.Title
                )
            ).ToList();
    }

    public async Task<ExpenseReport?> GetAsync(Guid id, CancellationToken ct)
    {
        return await reports.GetByIdAsync(id, ct);
    }
}