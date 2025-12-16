import { Component, inject, OnInit } from "@angular/core";
import { ExpenseReportListItemDto } from "../../../shared/models/expense-report.models";
import { UserListItemDto } from "../../../shared/models/user.models";
import { ExpenseReportsService } from "../../../core/services/expense-reports.service";
import { UsersService } from "../../../core/services/users.service";
import { FormsModule } from "@angular/forms";
import { RouterLink } from "@angular/router";
import {NavbarComponent} from "../../../shared/components/navbar/navbar.component";
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { SnackbarService } from "../../../core/services/snackbar.service";

@Component({
  selector: "app-expense-reports-page",
  imports: [
      FormsModule,
      RouterLink,
      NavbarComponent,
      MatTableModule,
      MatButtonModule,
      MatSelectModule,
      MatInputModule,
  ],
  templateUrl: "./expense-reports-page.html",
  styleUrl: "./expense-reports-page.scss",
})
export class ExpenseReportsPage implements OnInit {
  private readonly reportsService = inject(ExpenseReportsService);
  private readonly usersService = inject(UsersService);

  reports: ExpenseReportListItemDto[] = [];
  users: UserListItemDto[] = [];

  selectedUserId = "";
  year: number = new Date().getFullYear();
  month: number = new Date().getMonth() + 1;
  private readonly snackbarService = inject(SnackbarService);

  ngOnInit(): void {
    this.loadReports();
    this.loadUsers();
  }

  private loadReports(): void {
    this.reportsService.list().subscribe((data) => (this.reports = data));
  }

  private loadUsers(): void {
    // only active users
    this.usersService.list(true).subscribe((data) => (this.users = data));
  }

  create(): void {
    if (!this.selectedUserId) {
      this.snackbarService.error("User required to create an expense report");
      return;
    }

    this.reportsService
      .create({
        userId: this.selectedUserId,
        year: this.year,
        month: this.month,
      })
      .subscribe({
        next: () => this.loadReports(),
        error: err => {
          this.snackbarService.error(err.message);
        }
      });
  }

  delete(id: string): void {
    this.reportsService.delete(id).subscribe({
      next: () => this.loadReports(),
      error: err => {
        this.snackbarService.error(err.message);
      }
    });
  }
}
