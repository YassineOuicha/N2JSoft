namespace WebApi.Application.Dtos;

internal sealed record ExpenseReportListItemDto(
    Guid Id,
    Guid UserId,
    string UserDisplayName,
    int Year,
    int Month,
    string Title
);

internal sealed record CreateExpenseReportDto(
    Guid UserId,
    int Year,
    int Month
);

internal sealed record ExpenseReportDetailsDto(
    Guid Id,
    Guid UserId,
    string UserDisplayName,
    int Year,
    int Month,
    string Title
);