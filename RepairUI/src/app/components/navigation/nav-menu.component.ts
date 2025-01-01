import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterLink, RouterLinkActive } from "@angular/router";
import { TranslatePipe } from "../../pipes/translate.pipe";
import { AuthService } from "../../services/auth.service";

@Component({
  selector: "app-nav-menu",
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, TranslatePipe],
  template: `
    <nav
      class="fixed top-0 left-0 right-0 bg-gradient-to-r from-blue-500 to-purple-600 text-white p-4 z-50 shadow-md"
    >
      <div class="container mx-auto px-4">
        <div class="flex items-center justify-between h-16">
          <!-- Logo -->
          <div class="flex items-center space-x-3">
            <img
              src="https://via.placeholder.com/40"
              alt="Logo"
              class="h-10 w-10 rounded-full"
            />
            <a routerLink="/" class="text-2xl font-bold tracking-wide">
              {{ "nav.title" | translate }}
            </a>
          </div>

          <!-- Main Menu with Submenus -->
          <div class="hidden md:flex space-x-6 items-center">
            <!-- Repairs Dropdown -->
            <div class="relative group">
              <button class="px-4 py-2 hover:bg-blue-600 rounded-md">
                {{ "nav.repairs" | translate }}
              </button>
              <div
                class="absolute left-0 mt-2 w-48 bg-white text-gray-800 rounded-md shadow-lg opacity-0 transform scale-95 group-hover:opacity-100 group-hover:scale-100 transition-all duration-150 z-10"
              >
                <a
                  routerLink="/repairs"
                  routerLinkActive="bg-gray-100"
                  class="block px-4 py-2 hover:bg-gray-100"
                >
                  {{ "nav.repairList" | translate }}
                </a>
                <a
                  routerLink="/repairs/new"
                  routerLinkActive="bg-gray-100"
                  class="block px-4 py-2 hover:bg-gray-100"
                >
                  {{ "nav.newRepair" | translate }}
                </a>
              </div>
            </div>

            <!-- Users Dropdown -->
            <div class="relative group">
              <button class="px-4 py-2 hover:bg-blue-600 rounded-md">
                {{ "nav.users" | translate }}
              </button>
              <div
                class="absolute left-0 mt-2 w-48 bg-white text-gray-800 rounded-md shadow-lg opacity-0 transform scale-95 group-hover:opacity-100 group-hover:scale-100 transition-all duration-150 z-10"
              >
                <a
                  routerLink="/users"
                  routerLinkActive="bg-gray-100"
                  class="block px-4 py-2 hover:bg-gray-100"
                >
                  {{ "nav.userList" | translate }}
                </a>
                <a
                  routerLink="/users/new"
                  routerLinkActive="bg-gray-100"
                  class="block px-4 py-2 hover:bg-gray-100"
                >
                  {{ "nav.newUser" | translate }}
                </a>
              </div>
            </div>
          </div>

          <!-- User Menu -->
          <div class="flex items-center space-x-4">
            <button
              (click)="logout()"
              class="px-4 py-2 bg-red-600 hover:bg-red-700 rounded-md text-white"
            >
              {{ "nav.logout" | translate }}
            </button>
          </div>
        </div>
      </div>
    </nav>
  `,
})
export class NavMenuComponent {
  isAuthenticated$ = this.authService.isAuthenticated$;

  constructor(private authService: AuthService) {}

  logout() {
    this.authService.logout();
  }
}
