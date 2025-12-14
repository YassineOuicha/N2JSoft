export interface ExpenseReportListItemDto {
    id: string;
    userId: string;
    userDisplayName: string;
    year: number;
    month: number;
    title: string;
}

export type ExpenseReportDetailsDto = ExpenseReportListItemDto;

export interface CreateExpenseReportDto {
    userId: string;
    year: number;
    month: number;
}