export interface UserListItemDto {
    id: string;
    firstName: string;
    lastName: string;
    isActive: boolean;
    monthlyExpenseLimit: number;
}

export interface ExpenseReportListItemDto {
    id: string;
    userId: string;
    userDisplayName: string;
    year: number;
    month: number;
    title: string;
}

export interface CreateExpenseReportRequest {
    userId: string;
    year: number;
    month: number;
}

export interface ExpenseReportDetailDto {
    id: string;
    userId: string;
    userDisplayName: string;
    year: number;
    month: number;
    title: string;
}

export interface ExpenseListItemDto {
    id: string;
    date: string; // DateOnly serialized as "YYYY-MM-DD"
    description: string;
    amountEur: number;
    brand: string;
    street: string;
    postalCode: string;
    city: string;
}

export interface CreateExpenseRequest {
    date: string;
    description: string;
    amountEur: number;
    brand: string;
    street: string;
    postalCode: string;
    city: string;
}

export interface UpdateExpenseRequest extends CreateExpenseRequest {}

export interface PagedResultDto<T> {
    items: T[];
    page: number;
    pageSize: number;
    totalCount: number;
}