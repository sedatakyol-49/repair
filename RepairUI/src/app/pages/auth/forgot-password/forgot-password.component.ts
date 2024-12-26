import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  template: `
    <div class="min-h-screen flex items-center justify-center bg-repair-pattern bg-cover bg-center">
      <div class="absolute inset-0 bg-black/50 backdrop-blur-sm"></div>
      
      <div class="relative w-full max-w-md">
        <div class="bg-white rounded-2xl shadow-2xl p-8 m-4">
          <div class="text-center mb-8">
            <h2 class="text-3xl font-bold text-gray-800 mb-2">Reset Password</h2>
            <p class="text-gray-600">Enter your email to receive a temporary password</p>
          </div>
          
          <form [formGroup]="resetForm" (ngSubmit)="onSubmit()" class="space-y-6">
            <div>
              <label class="block text-sm font-medium text-gray-700 mb-1">Email</label>
              <div class="relative">
                <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                  <svg class="h-5 w-5 text-gray-400" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 12a4 4 0 10-8 0 4 4 0 008 0zm0 0v1.5a2.5 2.5 0 005 0V12a9 9 0 10-9 9m4.5-1.206a8.959 8.959 0 01-4.5 1.207" />
                  </svg>
                </div>
                <input 
                  type="email" 
                  formControlName="email"
                  class="pl-10 w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                  placeholder="Enter your email"
                >
              </div>
              <div *ngIf="resetForm.get('email')?.touched && resetForm.get('email')?.invalid" 
                   class="text-red-500 text-sm mt-1">
                Please enter a valid email address
              </div>
            </div>

            <div class="space-y-4">
              <button 
                type="submit"
                [disabled]="resetForm.invalid"
                class="w-full bg-blue-600 text-white py-2 px-4 rounded-lg hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
              >
                Send Reset Link
              </button>

              <div class="text-center">
                <a routerLink="/login" class="text-sm text-blue-600 hover:text-blue-500">
                  Back to Login
                </a>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  `
})
export class ForgotPasswordComponent {
  resetForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.resetForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  async onSubmit() {
    if (this.resetForm.valid) {
      const { email } = this.resetForm.value;
      try {
        await this.authService.resetPassword(email);
        this.router.navigate(['/reset-password']);
      } catch (error) {
        console.error('Error resetting password:', error);
      }
    }
  }
}