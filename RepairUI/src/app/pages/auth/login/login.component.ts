import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../../../services/auth.service';
import { ErrorService } from '../../../services/error.service';
import { LanguageSelectorComponent } from '../../../components/language-selector.component';
import { TranslatePipe } from '../../../pipes/translate.pipe';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule, 
    ReactiveFormsModule, 
    RouterLink, 
    LanguageSelectorComponent,
    TranslatePipe
  ],
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit, OnDestroy {
  loginForm: FormGroup;
  errorMessage = '';
  private errorSub?: Subscription;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private errorService: ErrorService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.checkBusinessExists();
    this.errorSub = this.errorService.errorMessage$.subscribe(
      error => this.errorMessage = error
    );
  }

  ngOnDestroy(): void {
    this.errorSub?.unsubscribe();
  }

  private checkBusinessExists(): void {
    this.authService.checkBusinessExists().subscribe({
      next: (exists: boolean) => {
        if (!exists) {
          this.router.navigate(['/register']);
        }
      },
      error: () => {
        this.errorService.setError('Failed to check business existence. Please try again later.');
      }
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;
      this.authService.login(email, password).subscribe({
        next: () => {
          this.errorService.clearError();
          this.router.navigate(['/repairs']);
        },
        error: () => {
          this.errorService.setError('Invalid email or password');
        }
      });
    }
  }
}