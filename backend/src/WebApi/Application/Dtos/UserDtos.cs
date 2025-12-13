namespace WebApi.Application.Dtos;

public sealed record UserListItemDto(
    Guid Id,
    string FirstName,
    string LastName,
    bool IsActive,
    int MonthlyExpenseLimit
);

public sealed record UserDetailDto(
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

public sealed record CreateUserDto(
    string FirstName,
    string LastName,
    string Street,
    string City,
    string PostalCode,
    string Country,
    int MonthlyExpenseLimit,
    bool IsActive
);

public sealed record UpdateUserDto(
    string FirstName,
    string LastName,
    string Street,
    string City,
    string PostalCode,
    string Country,
    int MonthlyExpenseLimit,
    bool IsActive
);