export interface ExpenseListItemDto {
    id: string;
    date: string;          // DateOnly : string "YYYY-MM-DD"
    description: string;
    amount: number;
    brand: string;
    street: string;
    city: string;
    postalCode: string;
}

export interface CreateExpenseDto {
    date: string;
    description: string;
    amount: number;
    brand: string;
    street: string;
    city: string;
    postalCode: string;
}

export type UpdateExpenseDto = CreateExpenseDto;

export interface PagedResultDto<T> {
    items: readonly T[];
    totalCount: number;
    pageNumber: number;
    pageSize: number;
}
