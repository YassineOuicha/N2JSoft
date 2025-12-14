import { inject, Injectable } from '@angular/core';
import { ApiClient } from "../api/api-client";
import { Observable } from "rxjs";
import {
  CreateExpenseReportDto,
  ExpenseReportDetailsDto,
  ExpenseReportListItemDto,
} from "../../shared/models/expense-report.models";

@Injectable({
    providedIn: 'root'
})
export class ExpenseReportsService {
    private readonly api = inject(ApiClient);

    list(): Observable<ExpenseReportListItemDto[]> {
        return this.api.get<ExpenseReportListItemDto[]>(`api/expense-reports`);
    }

    getById(id: string): Observable<ExpenseReportDetailsDto> {
        return this.api.get<ExpenseReportDetailsDto>(`api/expense-reports/${id}`);
    }

    create(dto: CreateExpenseReportDto): Observable<void> {
        return this.api.post<void>(`api/expense-reports`, dto);
    }

    delete(id: string): Observable<void> {
        return this.api.delete<void>(`api/expense-reports/${id}`);
    }
}
