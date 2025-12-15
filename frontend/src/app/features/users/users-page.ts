import { Component, inject, OnInit } from "@angular/core";
import { UsersService } from "../../core/services/users.service";
import { UserListItemDto } from "../../shared/models/user.models";
import { MatTableModule } from "@angular/material/table";
import { MatButtonModule } from "@angular/material/button";
import { NavbarComponent } from "../../shared/components/navbar/navbar.component";

@Component({
  selector: "app-users-page",
  imports: [MatTableModule, MatButtonModule, NavbarComponent],
  templateUrl: "./users-page.html",
  styleUrl: "./users-page.css",
})
export class UsersPage implements OnInit {
  private readonly usersService = inject(UsersService);
  users: UserListItemDto[] = [];
  onlyActive = true;
  error: string | null = null;

  displayedColumns = ["name", "limit", "actions"];

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.error = null;
    this.usersService.list(this.onlyActive).subscribe({
      next: (data) => (this.users = data),
      error: () => (this.error = "Error while loading users"),
    });
  }

  toggle(): void {
    this.onlyActive = !this.onlyActive;
    this.load();
  }

  delete(id: string): void {
    this.usersService.delete(id).subscribe({
      next: () => this.load(),
      error: () => (this.error = "Error while deleting user"),
    });
  }
}
