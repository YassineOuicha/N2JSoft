namespace WebApi.Application.Dtos;

public sealed record ExpenseReportListItemDto(
    Guid Id,
    Guid UserId,
    string UserDisplayName,
    int Year,
    int Month,
    string Title
);

public sealed record CreateExpenseReportDto(
    Guid UserId,
    int Year,
    int Month
);

public sealed record ExpenseReportDetailsDto(
    Guid Id,
    Guid UserId,
    string UserDisplayName,
    int Year,
    int Month,
    string Title
);