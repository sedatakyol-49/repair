import { Component } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink],
  template: `
    <nav class="bg-gray-800 text-white p-4" *ngIf="isAuthenticated$ | async">
      <div class="container mx-auto flex justify-between items-center">
        <a routerLink="/repairs" class="text-xl font-bold">Repair Tracker</a>
        <div class="space-x-4">
          <a routerLink="/repairs" class="hover:text-gray-300">Repairs</a>
          <a routerLink="/repairs/new" class="bg-blue-500 hover:bg-blue-600 px-4 py-2 rounded">New Repair</a>
          <button (click)="logout()" class="hover:text-gray-300">Logout</button>
        </div>
      </div>
    </nav>

    <main>
      <router-outlet></router-outlet>
    </main>
  `
})
export class AppComponent {
  isAuthenticated$ = this.authService.isAuthenticated$;

  constructor(private authService: AuthService) {}

  logout() {
    this.authService.logout();
  }
}