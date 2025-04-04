import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
    private apiUrl = 'https://localhost:5055/api/auth';
    // Replace with your backend API URL

  constructor(private http: HttpClient) {}


  login(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, data);
  }
  

  signup(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/signup`, data);
  }

  // login(email: string, password: string): Observable<any> {
  //   return this.http.post<any>(`${this.apiUrl}/login`, { email, password });
  // }

  logout() {
    localStorage.removeItem('token');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
