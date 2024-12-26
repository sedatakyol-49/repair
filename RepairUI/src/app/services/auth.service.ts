import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ApiService } from './api.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isAuthenticated = new BehaviorSubject<boolean>(false);
  isAuthenticated$ = this.isAuthenticated.asObservable();

  constructor(
    private api: ApiService,
    private router: Router
  ) {
    const token = localStorage.getItem('token');
    if (token) {
      this.isAuthenticated.next(true);
    }
  }

  checkBusinessExists(): Observable<boolean> {
    return this.api.get<boolean>('auth/business-exists').pipe(
      catchError(error => {
        console.error('Failed to check business existence:', error);
        return throwError(() => new Error('Failed to check business existence'));
      })
    );
  }

  login(email: string, password: string): Observable<{ token: string }> {
    return this.api.post<{ token: string }>('auth/login', { email, password }).pipe(
      tap(response => {
        localStorage.setItem('token', response.token);
        this.isAuthenticated.next(true);
      }),
      catchError(error => {
        console.error('Login failed:', error);
        return throwError(() => new Error('Invalid credentials'));
      })
    );
  }

  register(email: string, password: string, businessName: string): Observable<void> {
    return this.api.post<void>('auth/register', { email, password, businessName }).pipe(
      catchError(error => {
        console.error('Registration failed:', error);
        return throwError(() => new Error('Registration failed'));
      })
    );
  }

  resetPassword(email: string): Observable<void> {
    return this.api.post<void>('auth/reset-password', { email }).pipe(
      catchError(error => {
        console.error('Password reset failed:', error);
        return throwError(() => new Error('Failed to reset password'));
      })
    );
  }

  updatePassword(email: string, tempPassword: string, newPassword: string): Observable<void> {
    return this.api.post<void>('auth/update-password', {
      email,
      tempPassword,
      newPassword
    }).pipe(
      catchError(error => {
        console.error('Password update failed:', error);
        return throwError(() => new Error('Failed to update password'));
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.isAuthenticated.next(false);
    this.router.navigate(['/login']);
  }
}