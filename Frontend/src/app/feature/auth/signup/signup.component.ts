import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service'; // adjust the path if needed
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-signup',
  standalone: true,
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
  imports: [CommonModule, FormsModule,HttpClientModule],
  providers:[AuthService]
})
export class SignupComponent {
  username: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;
  isLoading: boolean = false;

  constructor(private router: Router, private authService: AuthService) {}

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  toggleConfirmPassword(): void {
    this.showConfirmPassword = !this.showConfirmPassword;
  }

  onSignUp(form: NgForm): void {
    if (form.invalid) return;

    if (this.password !== this.confirmPassword) {
      alert('Passwords do not match.');
      return;
    }

    this.isLoading = true;

    const payload = {
      username: this.username,
      email: this.email,
      password: this.password
    };

    this.authService.signUp(payload).subscribe({
      next: () => {
        this.isLoading = false;
        this.navigateToSignin();
      },
      error: (err) => {
        this.isLoading = false;
        alert('Signup failed. Please try again.');
        // console.error(err);
      }
    });
  }

  navigateToSignin() {
    this.router.navigate(['/auth/sign-in']);
  }
}
