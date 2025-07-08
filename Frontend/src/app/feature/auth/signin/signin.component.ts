import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service'; 
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  standalone: true,
  styleUrl: './signin.component.scss',
  imports: [CommonModule, FormsModule]
})
export class SigninComponent {
  email = '';
  password = '';
  rememberMe = false;
  isLoading = false;
  showPassword = false;

  constructor(private router: Router, private authService: AuthService) {}

  onSignIn(): void {
    if (!this.email || !this.password) return;

    this.isLoading = true;

    const payload = {
      identifier: this.email,
      password: this.password
    };

    this.authService.signIn(payload).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.authService.setToken(res.token);
        this.router.navigate(['/']);
      },
      error: (err) => {
        this.isLoading = false;
        alert('Login failed. Check credentials.');
        // console.error('Sign-in error:', err);
      }
    });
  }

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  navigateToSignup(): void {
    this.router.navigate(['/auth/sign-up']);
  }
}
