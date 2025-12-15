import { Component, inject, OnInit } from "@angular/core";
import { UsersService } from "../../core/services/users.service";
import { UserListItemDto } from "../../shared/models/user.models";
import { MatTableModule } from "@angular/material/table";
import { MatButtonModule } from "@angular/material/button";
import { NavbarComponent } from "../../shared/components/navbar/navbar.component";
import { CommonModule } from "@angular/common";
import {SnackbarService} from "../../core/services/snackbar.service";

@Component({
  selector: "app-users-page",
  imports: [CommonModule, MatTableModule, MatButtonModule, NavbarComponent],
  templateUrl: "./users-page.html",
  styleUrl: "./users-page.css",
})
export class UsersPage implements OnInit {
  private readonly usersService = inject(UsersService);
  private readonly snackbarService = inject(SnackbarService);
  users: UserListItemDto[] = [];
  onlyActive = true;

  displayedColumns = ["name", "limit", "actions"];

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.usersService.list(this.onlyActive).subscribe({
      next: (data) => (this.users = data),
      error: err => {
          this.snackbarService.error(err.message);
      }
    });
  }

  toggle(): void {
    this.onlyActive = !this.onlyActive;
    this.load();
  }

  delete(id: string): void {
    this.usersService.delete(id).subscribe({
      next: () => this.load(),
      error: err => {
          this.snackbarService.error(err.message);
      }
    });
  }
}
