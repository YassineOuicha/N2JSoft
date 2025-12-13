using WebApi.Domain.ValueObjects;

namespace WebApi.Domain.Entities;

public sealed class Expense
{
    public Guid Id { get; set; }
    public Guid ExpenseReportId { get; set; }
    public ExpenseReport ExpenseReport { get; set; } = null!;
    public DateOnly Date { get; set; }
    public required string Description { get; set; } // Max length 50
    public decimal Amount { get; set; } // Euro
    public required BillingAddress BillingAddress { get; set; }
    public bool IsDeleted { get; set; }
}