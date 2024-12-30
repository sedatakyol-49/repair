import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { AuthService } from "./services/auth.service";
import { HeaderComponent } from "./components/header/header.component";

@Component({
  selector: "app-root",
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  template: `
    <app-header></app-header>
    <main class="pt-16">
      <router-outlet></router-outlet>
    </main>
  `,
})
export class AppComponent {}
