import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class RegisterComponent {
  private authService = inject(AuthService);
  private router = inject(Router);
  private formBuilder = inject(FormBuilder);

  registerForm = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    username: ['', [Validators.required, Validators.minLength(3)]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  onSubmit() {
    if (this.registerForm.invalid) {
      return;
    }

    const credentials = {
      email: this.registerForm.value.email ?? '',
      password: this.registerForm.value.password ?? '',
      userName: this.registerForm.value.username ?? '',
    };

    this.authService.register(credentials).subscribe({
      next: () => {
        this.router.navigateByUrl('/home');
      },
      error: (err) => {
        console.error('Registration error:', err);
      }
    });
  }
}
