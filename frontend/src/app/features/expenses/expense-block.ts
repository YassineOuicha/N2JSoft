import { Component, inject, Input, OnInit } from "@angular/core";
import {ExpensesService} from "../../core/services/expenses.service";
import {ExpenseListItemDto} from "../../shared/models/expense.models";
import {FormsModule} from "@angular/forms";
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import {MatFormField, MatInput} from '@angular/material/input';
import {NavbarComponent} from "../../shared/components/navbar/navbar.component";


@Component({
  selector: "app-expense-block",
  imports: [
    FormsModule,
    MatTableModule,
    MatButtonModule,
    MatTableModule,
    MatFormField,
    NavbarComponent,
    MatInput,
  ],
  templateUrl: "./expense-block.html",
  styleUrl: "./expense-block.css",
})
export class ExpenseBlock implements OnInit {
  @Input({ required: true }) reportId: string;
  private readonly expensesService = inject(ExpensesService);
  expenses: ExpenseListItemDto[] = [];
  pageNumber = 1;
  pageSize = 5;
  totalCount = 0;
  description = "";
  amount = 0;
  brand = "";
  street = "";
  city = "";
  postalCode = "";

  error: string | null = null;

  ngOnInit() {
    this.load();
  }

  load(): void {
    this.expensesService
      .listByReport(this.reportId, this.pageNumber)
      .subscribe({
        next: (result) => {
          this.expenses = result.items;
          this.totalCount = result.totalCount;
          this.pageNumber = result.pageSize;
        },
        error: (err) => {
          this.error = err.message;
          console.log(this.error);
        },
      });
  }

  create(): void {
    this.error = null;
    this.expensesService
      .create(this.reportId, {
        date: new Date().toISOString().substring(0, 10),
        description: this.description,
        amount: this.amount,
        brand: this.brand,
        street: this.street,
        city: this.city,
        postalCode: this.postalCode,
      })
      .subscribe({
        next: () => {
          this.reset();
          this.load();
        },
        error: (err) => {
          this.error =
            err.status === 400
              ? "Monthly quota reached"
              : "Error creating expense";
          console.log(this.error);
        },
      });
  }

  delete(id: string): void {
    this.expensesService.delete(id).subscribe({
      next: () => this.load(),
      error: (err) => {
        this.error = err.message;
        console.log(err);
      },
    });
  }

  next(): void {
    this.pageNumber += 1;
    this.load();
  }

  prev(): void {
    this.pageNumber -= 1;
    this.load();
  }

  private reset() {
    this.description = "";
    this.amount = 0;
    this.brand = "";
    this.street = "";
    this.city = "";
    this.postalCode = "";
  }
}
