import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrls: ['./register.css']
})
export class RegisterComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  registerForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  onSubmit() {
    if (this.registerForm.valid) {
      const { email, password } = this.registerForm.value;

      this.authService.register(email!, password!).subscribe({
        next: () => {
          alert('Account created successfully!');
          this.router.navigate(['/dashboard']);
        },
        error: (err) => {
          console.error('Registration failed', err);
          const errorMsg = err.error?.[0] || 'Registration failed';
          alert(errorMsg);
        }
      });
    }
  }
}
