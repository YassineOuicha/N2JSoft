using WebApi.Domain.ValueObjects;

namespace WebApi.Domain.Entities;

public sealed class User
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    
    public required PostalAddress PostalAddress { get; set; }

    public bool IsActive { get; set; } = true;

    public int MonthlyExpenseLimit { get; set; } = 10;

    public bool IsDeleted { get; set; }
    
    public ICollection<ExpenseReport> ExpenseReports { get; set; } = new List<ExpenseReport>();
}