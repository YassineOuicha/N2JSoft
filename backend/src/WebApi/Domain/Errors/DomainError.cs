namespace WebApi.Domain.Errors;

public sealed record DomainError(string Code, string Message);