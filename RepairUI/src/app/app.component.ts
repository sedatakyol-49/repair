import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { AuthService } from "./services/auth.service";
import { HeaderComponent } from "./components/header/header.component";
import { NavMenuComponent } from "./components/navigation/nav-menu.component";
import { AsyncPipe, NgIf } from "@angular/common";

@Component({
  selector: "app-root",
  standalone: true,
  imports: [RouterOutlet, NavMenuComponent, NgIf, AsyncPipe],
  template: `
    <div class="min-h-screen bg-repair-pattern">
      <app-nav-menu *ngIf="isAuthenticated$ | async"></app-nav-menu>
      <main class="pt-16">
        <router-outlet></router-outlet>
      </main>
    </div>
  `,
})
export class AppComponent {
  isAuthenticated$ = this.authService.isAuthenticated$;

  constructor(private authService: AuthService) {}
}
