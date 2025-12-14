import { Component, inject, OnInit } from "@angular/core";
import { UsersService } from "../../core/services/users.service";
import { UserListItemDto } from "../../shared/models/user.models";

@Component({
  selector: "app-users-page",
  imports: [],
  templateUrl: "./users-page.html",
  styleUrl: "./users-page.css",
})
export class UsersPage implements OnInit {
  private readonly usersService = inject(UsersService);
  users: UserListItemDto[] = [];
  onlyActive = true;
  error: string | null = null;

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.error = null;
    this.usersService.list(this.onlyActive).subscribe({
      next: data => this.users = data,
      error: () => this.error = 'Error while loading users'
    });
  }

  toggle(): void {
    this.onlyActive= !this.onlyActive;
    this.load();
  }

  delete(id: string): void {
    this.usersService.delete(id).subscribe({
      next: () => this.load(),
      error: ()=> this.error = 'Error while deleting user'
    })
  }
}
