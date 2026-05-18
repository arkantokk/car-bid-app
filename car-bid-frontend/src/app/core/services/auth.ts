import {inject, Injectable, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, tap} from 'rxjs';
import {jwtDecode} from 'jwt-decode';

export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthResponse {
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5044/api/Auth';
  currentUser = signal<string | null>(null);

  constructor() {
    this.loadUserFromToken();
  }

  login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, credentials)
      .pipe(
        tap((response) => {
          jwtDecode(response.token)
          localStorage.setItem('token', response.token);
          this.loadUserFromToken();
        })
      );
  }

  register(credentials: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, credentials)
      .pipe(
        tap((response) => {
          localStorage.setItem('token', response.token);
          this.loadUserFromToken();
        })
      );
  }

  logout() {
    this.http.post(`${this.apiUrl}/logout`, {})
      .pipe(
        tap((response) => {
          localStorage.removeItem('token');
          this.currentUser.set(null);
        })
      ).subscribe()
    // for the future when backend will be updated
  }

  private loadUserFromToken() {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decodedToken: any = jwtDecode(token);
        const username = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
        this.currentUser.set(username || 'User');
      } catch (error) {
        console.error('Failed to decode token', error);
        this.logout();
      }
    }
  }
}
