import { Injectable, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';

interface LoginResponse { token: string; role: string; }

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly API = 'http://localhost:5197';
  private _token = signal<string | null>(localStorage.getItem('token'));
  private _role = signal<string | null>(localStorage.getItem('role'));

  isLoggedIn = computed(() => !!this._token());
  isAdmin = computed(() => this._role() === 'Admin');
  token = this._token.asReadonly();

  constructor(private http: HttpClient) {}

  login(username: string, password: string) {
    return this.http.post<LoginResponse>(`${this.API}/auth/login`, { username, password }).pipe(
      tap(res => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('role', res.role);
        this._token.set(res.token);
        this._role.set(res.role);
      })
    );
  }

  logout() {
    localStorage.clear();
    this._token.set(null);
    this._role.set(null);
  }
}
