import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { Router, RouterLink } from "@angular/router";
import { AuthService } from "../../../services/auth.service";
import { ErrorService } from "../../../services/error.service";
import { TranslatePipe } from "../../../pipes/translate.pipe";
import { LanguageSelectorComponent } from "../../../components/language-selector.component";

@Component({
  selector: "app-register",
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    TranslatePipe,
    LanguageSelectorComponent,
  ],
  templateUrl: "./register.component.html",
})
export class RegisterComponent {
  registerForm: FormGroup;
  errorMessage = "";

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private errorService: ErrorService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      firmName: ["", [Validators.required, Validators.minLength(3)]],
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required, Validators.minLength(6)]],
    });

    this.errorService.errorMessage$.subscribe(
      (error) => (this.errorMessage = error)
    );
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      const { email, password, businessName } = this.registerForm.value;

      this.authService.register(email, password, businessName).subscribe({
        next: () => {
          this.errorService.clearError();
          this.router.navigate(["/login"]);
        },
        error: (error) => {
          this.errorService.setError(error.message || "Registration failed");
        },
      });
    }
  }
}
