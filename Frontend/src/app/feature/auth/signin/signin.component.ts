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
  imports: [CommonModule, FormsModule,HttpClientModule],
  providers:[AuthService]
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
        // console.log('Sign-in successful:', res);
        // TODO: Store token, handle user info, etc.
        this.router.navigate(['/blockly']); // Or wherever you want to redirect
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

  signInWithGoogle(): void {
    console.log('Google Sign-In clicked');
    // TODO: Integrate Google Sign-In
  }

  signInWithGithub(): void {
    console.log('GitHub Sign-In clicked');
    // TODO: Integrate GitHub Sign-In
  }

  navigateToSignup(): void {
    this.router.navigate(['/auth/sign-up']);
  }
}
