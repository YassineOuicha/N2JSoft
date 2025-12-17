import { Component, inject } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { MatInputModule } from "@angular/material/input";
import { MatButtonModule } from "@angular/material/button";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { CreateExpenseDto } from "../../../shared/models/expense.models";

export interface ExpenseDialogData {
    title: string;
    expense?: CreateExpenseDto;
}

@Component({
    selector: "app-expense-dialog",
    imports: [
        CommonModule,
        FormsModule,
        MatInputModule,
        MatButtonModule
    ],
    templateUrl: "./expense-dialog.component.html",
    styleUrl: "./expense-dialog.component.scss",
})
export class ExpenseDialogComponent {
    private readonly dialogRef = inject(MatDialogRef<ExpenseDialogComponent>);
    readonly data = inject<ExpenseDialogData>(MAT_DIALOG_DATA);

    model: CreateExpenseDto = this.data.expense ?? {
        date: new Date().toISOString().substring(0, 10),
        description: "",
        amount: 0,
        brand: "",
        street: "",
        city: "",
        postalCode: ""
    };

    save(): void {
        this.dialogRef.close(this.model);
    }

    cancel(): void {
        this.dialogRef.close();
    }
}