import { Component } from "@angular/core";
import { AuthService } from "../../services/auth.service";
import { CommonModule } from "@angular/common";
import { RouterLink } from "@angular/router";

@Component({
  selector: "app-header",
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: "./header.component.html",
  styleUrl: "./header.component.css",
})
export class HeaderComponent {
  isAuthenticated$ = this.authService.isAuthenticated$;

  constructor(private authService: AuthService) {}

  logout() {
    this.authService.logout();
  }
}
