import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'http://localhost:5000/api/auth'; // 🔁 Replace with your .NET backend URL
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient, private router: Router) {}

  // Getter for login status
  getLoginStatus(): Observable<boolean> {
    return this.isLoggedInSubject.asObservable();
  }

  setLoginStatus(status: boolean) {
    this.isLoggedInSubject.next(status);
  }

  // 🔐 LOGIN
  login(email: string, password: string): Observable<any> {
    const loginPayload = { email, password };
    return this.http.post(`${this.baseUrl}/login`, loginPayload);
  }
  sendResetLink(email: string) {
    return this.http.post(`${this.baseUrl}/forgot-password`, { email });
  }
  

  // 🧾 SIGNUP
  signup(name: string, email: string, password: string): Observable<any> {
    const signupPayload = { name, email, password };
    return this.http.post(`${this.baseUrl}/register`, signupPayload);
  }

  // 🚪 LOGOUT
  logout() {
    this.setLoginStatus(false);
    this.router.navigate(['/home']);
    localStorage.removeItem('token');
  }

  // Optional: Store JWT token
  storeToken(token: string) {
    localStorage.setItem('token', token);
    this.setLoginStatus(true);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}
