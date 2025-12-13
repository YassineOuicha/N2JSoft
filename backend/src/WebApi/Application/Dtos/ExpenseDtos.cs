namespace WebApi.Application.Dtos;

public sealed record ExpenseListItemDto(
    Guid Id,
    DateOnly Date,
    string Description,
    decimal Amount,
    string Brand,
    string Street,
    string City,
    string PostalCode
);

public sealed record CreateExpenseDto(
    DateOnly Date,
    string Description,
    decimal Amount,
    string Brand,
    string Street,
    string City,
    string PostalCode
);

public sealed record UpdateExpenseDto( 
    DateOnly Date,
    string Description,
    decimal Amount,
    string Brand,
    string Street,
    string City,
    string PostalCode
);

public sealed record PagedResultDto<T>(
    IReadOnlyList<T> Items,
    int TotalCount,
    int PageNumber,
    int PageSize
);