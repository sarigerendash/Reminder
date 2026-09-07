import { Component, signal } from '@angular/core';
import { AuthService } from './auth';
import { Login } from './login/login';
import { Reminders } from './reminders/reminders';
import { ReminderHistory } from './history/history';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [Login, Reminders, ReminderHistory],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  view = signal<'list' | 'history'>('list');

  constructor(public auth: AuthService) {}
}
