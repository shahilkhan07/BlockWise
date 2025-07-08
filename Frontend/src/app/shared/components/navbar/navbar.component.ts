import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../feature/services/auth.service';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit{
  isMobileMenuOpen = false;
  isLoggedIn: boolean = false;
  private authSubscription!: Subscription;

  constructor(private router:Router, private authService: AuthService){}
  ngOnInit(){
    this.authSubscription = this.authService.loggedIn$.subscribe((status) => {
      this.isLoggedIn = status;
    });
  }

  toggleMobileMenu() {
    this.isMobileMenuOpen = !this.isMobileMenuOpen;
  }

  closeMobileMenu() {
    this.isMobileMenuOpen = false;
  }

  onLogin() {
    this.router.navigate(['/auth/sign-in']);
  }

  onLogout(){
    this.authService.logout();
    this.isLoggedIn = false;
  }

  onAboutUs() {
    this.router.navigate(['/about-us']);
  }

  onRegister() {
    this.router.navigate(['/auth/sign-up']);
  }

  ngOnDestroy() {
    if (this.authSubscription) {
      this.authSubscription.unsubscribe();
    }
  }
}
