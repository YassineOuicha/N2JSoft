import { Component, inject, Input, OnInit } from "@angular/core";
import {ExpensesService} from "../../core/services/expenses.service";
import {ExpenseListItemDto} from "../../shared/models/expense.models";
import {FormsModule} from "@angular/forms";
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { SnackbarService } from "../../core/services/snackbar.service";
import { MatPaginatorModule, PageEvent } from "@angular/material/paginator";
import { ExpenseDialogComponent } from "./dialog/expense-dialog.component";
import { MatDialog } from "@angular/material/dialog";

@Component({
  selector: "app-expense-block",
  imports: [
    FormsModule,
    MatTableModule,
    MatButtonModule,
    MatTableModule,
    MatPaginatorModule
  ],
  templateUrl: "./expense-block.html",
  styleUrl: "./expense-block.scss",
})
export class ExpenseBlock implements OnInit {
  @Input({ required: true }) reportId: string;
  private readonly expensesService = inject(ExpensesService);
  private readonly snackbarService = inject(SnackbarService);

  private readonly dialog = inject(MatDialog);

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
          this.pageNumber = result.pageNumber;
        },
        error: err => {
          this.snackbarService.error(err.error);
        }
      });
  }

  create(): void {
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
        error: err => {
          this.snackbarService.error(err.error);
        }
      });
  }

  delete(id: string): void {
    this.expensesService.delete(id).subscribe({
      next: () => this.load(),
      error: err => {
        this.snackbarService.error(err.error);
      }
    });
  }

  onPageChange(event: PageEvent): void{
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.load();
  }

  openCreateDialog(): void {
    const ref = this.dialog.open(ExpenseDialogComponent, {
      data: { title: "Add expense" }
    });

    ref.afterClosed().subscribe(dto => {
      if (!dto) return;

      this.expensesService.create(this.reportId, dto).subscribe({
        next: () => {
          this.snackbarService.success("Expense created");
          this.load();
        },
        error: err => this.snackbarService.error(err.error)
      });
    });
  }

  openEditDialog(expense: ExpenseListItemDto): void {
    const ref = this.dialog.open(ExpenseDialogComponent, {
      data: {
        title: "Edit expense",
        expense: { ...expense }
      }
    });

    ref.afterClosed().subscribe(dto => {
      if (!dto) return;

      this.expensesService.update(expense.id, dto).subscribe({
        next: () => {
          this.snackbarService.success("Expense updated");
          this.load();
        },
        error: err => this.snackbarService.error(err.error)
      });
    });
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
