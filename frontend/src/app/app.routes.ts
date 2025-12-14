import { Route } from '@angular/router';
import { HomePage } from "./layout/home/home-page";
import { UsersPage } from "./features/users/users-page";
import { ExpenseReportsPage } from "./features/expense-reports/page/expense-reports-page";
import { ExpenseReportsDetailPage } from "./features/expense-reports/detail/expense-reports-detail-page";

export const appRoutes: Route[] = [
  {path: '', component: HomePage},
  {path: 'users', component: UsersPage},
  {path: 'expense-reports', component: ExpenseReportsPage},
  {path: 'expense-reports/:id', component: ExpenseReportsDetailPage}
];
