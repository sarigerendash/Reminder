import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';
import { Reminder, ReminderRequest, ReminderRun } from './models';

@Injectable({ providedIn: 'root' })
export class ReminderService {
  private readonly API = 'http://localhost:5197/reminders';
  reminders = signal<Reminder[]>([]);
  history = signal<ReminderRun[]>([]);

  constructor(private http: HttpClient) {}

  loadAll() {
    return this.http.get<Reminder[]>(this.API).pipe(
      tap(data => this.reminders.set(data))
    );
  }

  create(req: ReminderRequest) {
    return this.http.post<Reminder>(this.API, req).pipe(
      tap(r => this.reminders.update(list => [...list, r]))
    );
  }

  update(id: number, req: ReminderRequest) {
    return this.http.put<Reminder>(`${this.API}/${id}`, req).pipe(
      tap(r => this.reminders.update(list => list.map(x => x.id === id ? r : x)))
    );
  }

  loadHistory() {
    return this.http.get<ReminderRun[]>(`${this.API}/history`).pipe(
      tap(data => this.history.set(data))
    );
  }
}
