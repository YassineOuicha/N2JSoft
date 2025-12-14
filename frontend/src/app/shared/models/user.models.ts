export interface UserListItemDto {
    id: string;
    firstName: string;
    lastName: string;
    isActive: boolean;
    monthlyExpenseLimit: number;
}

export interface UserDetailDto extends UserListItemDto {
    street: string;
    city: string;
    postalCode: string;
    country: string;
}

export interface CreateUserDto {
    firstName: string;
    lastName: string;
    street: string;
    city: string;
    postalCode: string;
    country: string;
    monthlyExpenseLimit: number;
    isActive: boolean;
}

export type UpdateUserDto = CreateUserDto;