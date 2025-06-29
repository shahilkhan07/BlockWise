import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'http://localhost:59500/v1/Auth';
  private tokenKey = 'access_token';

  constructor(private http: HttpClient) {}

  signUp(payload: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/signUp`, payload);
  }

  signIn(payload: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/signIn`, payload);
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
