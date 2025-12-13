namespace WebApi.Application.Dtos;

internal sealed record UserListItemDto(
    Guid Id,
    string FirstName,
    string LastName,
    bool IsActive,
    int MonthlyExpenseLimit
);

internal sealed record UserDetailDto(
    Guid Id,
    string FirstName,
    string LastName,
    bool IsActive,
    int MonthlyExpenseLimit,
    string Street,
    string City,
    string PostalCode,
    string Country
);

internal sealed record CreateUserDto(
    string FirstName,
    string LastName,
    string Street,
    string City,
    string PostalCode,
    string Country,
    int MonthlyExpenseLimit,
    bool IsActive
);

internal sealed record UpdateUserDto(
    string FirstName,
    string LastName,
    string Street,
    string City,
    string PostalCode,
    string Country,
    int MonthlyExpenseLimit,
    bool IsActive
);