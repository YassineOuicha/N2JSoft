import { Component, inject, OnInit } from "@angular/core";
import { UsersService } from "../../core/services/users.service";
import { UserListItemDto } from "../../shared/models/user.models";
import { MatTableModule } from "@angular/material/table";
import { MatButtonModule } from "@angular/material/button";
import { NavbarComponent } from "../../shared/components/navbar/navbar.component";
import { CommonModule } from "@angular/common";
import {SnackbarService} from "../../core/services/snackbar.service";
import { MatDialog } from '@angular/material/dialog';
import { UserDialogComponent } from "./dialog/user-dialog.component";

@Component({
  selector: "app-users-page",
  imports: [CommonModule, MatTableModule, MatButtonModule, NavbarComponent],
  templateUrl: "./users-page.html",
  styleUrl: "./users-page.scss",
})
export class UsersPage implements OnInit {
  private readonly usersService = inject(UsersService);
  private readonly snackbarService = inject(SnackbarService);
  private readonly dialog = inject(MatDialog);

  users: UserListItemDto[] = [];
  onlyActive = true;

  displayedColumns = ["name", "limit", "actions"];

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.usersService.list(this.onlyActive).subscribe({
      next: (data) => (this.users = data),
      error: (err) => {
        this.snackbarService.error(err.message);
      },
    });
  }

  toggle(): void {
    this.onlyActive = !this.onlyActive;
    this.load();
  }

  openCreateDialog(): void {
    const ref = this.dialog.open(UserDialogComponent, {
      data: { title: 'Create user' }
    });

    ref.afterClosed().subscribe(dto => {
      if (!dto) return;

      this.usersService.create(dto).subscribe({
        next: () => {
          this.usersService.create(dto);
          this.snackbarService.success('User created');
          this.load();
        },
        error: err => this.snackbarService.error(err.message)
      });
    });
  }

  openEditDialog(u: UserListItemDto): void {
    this.usersService.getById(u.id).subscribe(user => {
      const ref = this.dialog.open(UserDialogComponent, {
        data: {
          title: 'Edit user',
          user
        }
      });

      ref.afterClosed().subscribe(dto => {
        if (!dto) return;

        this.usersService.update(u.id, dto).subscribe({
          next: () => {
            this.snackbarService.success('User updated');
            this.load();
          },
          error: err => this.snackbarService.error(err.message)
        });
      });
    });
  }


  delete(id: string): void {
    this.usersService.delete(id).subscribe({
      next: () => this.load(),
      error: (err) => {
        this.snackbarService.error(err.message);
      },
    });
  }
}
