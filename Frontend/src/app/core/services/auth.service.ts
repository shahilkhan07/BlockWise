import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private baseUrl = 'https://localhost:5001/api/auth';
  private tokenKey = 'access_token';

  constructor(private http: HttpClient) {}

  login(data: { email: string; password: string }) {
    return this.http.post(`${this.baseUrl}/login`, data);
  }

  register(data: { name: string; email: string; password: string }) {
    return this.http.post(`${this.baseUrl}/register`, data);
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
  }

  setToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
