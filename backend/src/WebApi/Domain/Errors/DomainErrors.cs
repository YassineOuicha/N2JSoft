namespace WebApi.Domain.Errors;

public static class DomainErrors
{
    public static DomainError UserInactive(Guid userId) =>
        new("user.inactive", $"User '{userId}' is inactive and cannot be assigned.");

    public static DomainError UserDeleted(Guid userId) =>
        new("user.deleted", $"User '{userId}' is deleted and cannot be assigned.");

    public static DomainError ExpenseDescriptionTooLong() =>
        new("expense.description.maxlength", "Description must be 50 characters or less.");

    public static DomainError MonthlyQuotaReached(int limit) =>
        new("expense.quota.reached", $"Monthly expense limit reached ({limit}).");
}