import { inject, Injectable } from '@angular/core';
import { ApiClient } from "../api/api-client";
import { Observable } from "rxjs";
import {
  CreateExpenseDto,
  ExpenseListItemDto,
  PagedResultDto,
  UpdateExpenseDto,
} from "../../shared/models/expense.models";

@Injectable({
    providedIn: 'root'
})
export class ExpensesService {
    private readonly api = inject(ApiClient);

    listByReport(reportId: string, pageNumber: number): Observable<PagedResultDto<ExpenseListItemDto[]>> {
        return this.api.get<PagedResultDto<ExpenseListItemDto[]>>(
            `api/expenses/by-report/${reportId}?pageNumber=${pageNumber}`);
    }

    create(reportId: string, dto: CreateExpenseDto): Observable<void> {
        return this.api.post<void>(`api/expenses/by-report/${reportId}`, dto);
    }

    update(id: string, dto: UpdateExpenseDto): Observable<void> {
        return this.api.put<void>(`api/expenses/${id}`, dto);
    }

    delete(id: string): Observable<void> {
        return this.api.delete<void>(`api/expenses/${id}`);
    }
}
