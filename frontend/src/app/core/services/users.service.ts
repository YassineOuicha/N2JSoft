import { inject, Injectable } from '@angular/core';
import { ApiClient } from "../api/api-client";
import { Observable } from "rxjs";
import {
  CreateUserDto,
  UpdateUserDto,
  UserDetailDto,
  UserListItemDto,
} from "../../shared/models/user.models";

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private readonly api = inject(ApiClient);

  list(onlyActive: boolean): Observable<UserListItemDto[]> {
    const qs = onlyActive? '?onlyActive=true': '';
    return this.api.get<UserListItemDto[]>(`api/users${qs}`);
  }

  getById(id: string): Observable<UserDetailDto> {
    return this.api.get<UserDetailDto>(`api/users/${id}`);
  }

  create(dto: CreateUserDto): Observable<void> {
    return this.api.post<void>(`api/users`, dto);
  }

  update(id: string, dto: UpdateUserDto): Observable<void> {
    return this.api.put<void>(`api/users/${id}`, dto);
  }

  delete(id: string): Observable<void> {
    return this.api.delete<void>(`api/users/${id}`);
  }
}
