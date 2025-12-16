namespace WebApi.Domain.Errors;

public static class ExpensePolicy
{ 
    public static DomainError? ValidateDescription(string description)
    {
        return description.Length > 50 ? DomainErrors.ExpenseDescriptionTooLong() : null;
    }
}