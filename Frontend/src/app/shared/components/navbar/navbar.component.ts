import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  isMobileMenuOpen = false;

  constructor(private router:Router){}
  toggleMobileMenu() {
    this.isMobileMenuOpen = !this.isMobileMenuOpen;
  }

  closeMobileMenu() {
    this.isMobileMenuOpen = false;
  }

  onLogin() {
    this.router.navigate(['/auth/sign-in']);
  }

  onAboutUs() {
    this.router.navigate(['/about-us']);
  }

  onRegister() {
    this.router.navigate(['/auth/sign-up']);
  }
}
