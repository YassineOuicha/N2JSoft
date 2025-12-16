import { Component, inject} from "@angular/core";
import {CreateUserDto} from "../../../shared/models/user.models";
import {CommonModule} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import {MatCheckboxModule} from "@angular/material/checkbox";
import { MatButtonModule } from "@angular/material/button";
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

export interface UserDialogData {
    title: string;
    user?: CreateUserDto;
}

@Component({
    selector: "app-user-dialog",
    imports: [
        CommonModule,
        FormsModule,
        MatInputModule,
        MatCheckboxModule,
        MatButtonModule
    ],
    templateUrl: "./user-dialog.component.html",
    styleUrl: "./user.dialog.component.scss",
})
export class UserDialogComponent{
    private readonly dialogRef = inject(MatDialogRef<UserDialogComponent>);
    readonly data = inject<UserDialogData>(MAT_DIALOG_DATA);

    model: CreateUserDto = this.data.user ?? {
        firstName: '',
        lastName: '',
        street: '',
        city: '',
        postalCode: '',
        country: '',
        monthlyExpenseLimit: 10,
        isActive: true,
    };

    save(): void{
        this.dialogRef.close(this.model);
    }

    cancel(): void{
        this.dialogRef.close();
    }
}
