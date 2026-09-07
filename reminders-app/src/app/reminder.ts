import { Injectable, signal } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap } from 'rxjs';
import { Reminder, ReminderRequest, ReminderRun } from './models';
import { AuthService } from './auth';

@Injectable({ providedIn: 'root' })
export class ReminderService {
  private readonly API = 'http://localhost:5197/reminders';
  reminders = signal<Reminder[]>([]);
  history = signal<ReminderRun[]>([]);

  constructor(private http: HttpClient, private auth: AuthService) {}

  private headers() {
    return new HttpHeaders({ Authorization: `Bearer ${this.auth.token()}` });
  }

  loadAll() {
    return this.http.get<Reminder[]>(this.API, { headers: this.headers() }).pipe(
      tap(data => this.reminders.set(data))
    );
  }

  create(req: ReminderRequest) {
    return this.http.post<Reminder>(this.API, req, { headers: this.headers() }).pipe(
      tap(r => this.reminders.update(list => [...list, r]))
    );
  }

  update(id: number, req: ReminderRequest) {
    return this.http.put<Reminder>(`${this.API}/${id}`, req, { headers: this.headers() }).pipe(
      tap(r => this.reminders.update(list => list.map(x => x.id === id ? r : x)))
    );
  }

  loadHistory() {
    return this.http.get<ReminderRun[]>(`${this.API}/history`, { headers: this.headers() }).pipe(
      tap(data => this.history.set(data))
    );
  }
}
