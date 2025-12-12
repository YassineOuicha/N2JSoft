using WebApi.Domain.ValueObjects;

namespace WebApi.Domain.Entities;

internal sealed class User
{
    public Guid Id { get; set; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    
    public required PostalAddress PostalAddress { get; set; }

    public bool IsActive { get; set; } = true;

    public int MonthlyExpenseLimit { get; set; } = 10;

    public bool IsDeleted { get; set; }
}