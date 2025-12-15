import { Component, inject, OnInit } from "@angular/core";
import { ExpenseReportListItemDto } from "../../../shared/models/expense-report.models";
import { ActivatedRoute } from "@angular/router";
import { ExpenseReportsService } from "../../../core/services/expense-reports.service";
import { ExpenseBlock } from "../../expenses/expense-block";
import {NavbarComponent} from "../../../shared/components/navbar/navbar.component";

@Component({
  selector: "app-expense-reports-detail-page",
  imports: [ExpenseBlock, NavbarComponent],
  templateUrl: "./expense-reports-detail-page.html",
  styleUrl: "./expense-reports-detail-page.css",
})
export class ExpenseReportsDetailPage implements OnInit {
  report: ExpenseReportListItemDto | null = null;
  private readonly route = inject(ActivatedRoute);
  private readonly reportsService = inject(ExpenseReportsService);

  error: string | null = null;

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get("id");
    if (!id) {
      return;
    }

    this.reportsService.getById(id).subscribe({
      next: (data) => (this.report = data),
      error: (err) => {
        this.error = err.message;
        console.log(this.error);
      },
    });
  }
}
