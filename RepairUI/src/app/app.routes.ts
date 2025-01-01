import { Routes } from "@angular/router";
import { authGuard } from "./guards/auth.guard";

export const routes: Routes = [
  {
    path: "",
    redirectTo: "repairs",
    pathMatch: "full",
  },
  {
    path: "login",
    loadComponent: () =>
      import("./pages/auth/login/login.component").then(
        (m) => m.LoginComponent
      ),
  },
  {
    path: "register",
    loadComponent: () =>
      import("./pages/auth/register/register.component").then(
        (m) => m.RegisterComponent
      ),
  },
  {
    path: "forgot-password",
    loadComponent: () =>
      import("./pages/auth/forgot-password/forgot-password.component").then(
        (m) => m.ForgotPasswordComponent
      ),
  },
  {
    path: "reset-password",
    loadComponent: () =>
      import("./pages/auth/reset-password/reset-password.component").then(
        (m) => m.ResetPasswordComponent
      ),
  },
  {
    path: "repairs",
    canActivate: [authGuard],
    children: [
      {
        path: "",
        loadComponent: () =>
          import("./pages/repairs/repair-list/repair-list.component").then(
            (m) => m.RepairListComponent
          ),
      },
      {
        path: "new",
        loadComponent: () =>
          import("./pages/repairs/repair-create/repair-create.component").then(
            (m) => m.RepairCreateComponent
          ),
      },
      {
        path: ":id",
        loadComponent: () =>
          import("./pages/repairs/repair-detail/repair-detail.component").then(
            (m) => m.RepairDetailComponent
          ),
      },
      {
        path: ":id/edit",
        loadComponent: () =>
          import("./pages/repairs/repair-edit/repair-edit.component").then(
            (m) => m.RepairEditComponent
          ),
      },
    ],
  },
];
