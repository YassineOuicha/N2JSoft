namespace WebApi.Domain.Entities;

public sealed class ExpenseReport
{
    public Guid Id { get; set; }
    public required Guid UserId { get; init; }
    public User User { get; set; } = null!;
    
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
   
    // Not editable after creation
    public required string Title { get; set; }
    
    // Here we are only interested in year and month no need for full date to avoid complexity
    public int Year { get; set; }
    public int Month { get; set; }
    
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}