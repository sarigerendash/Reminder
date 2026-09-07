import { Component } from '@angular/core';
import { AuthService } from './auth';
import { Login } from './login/login';
import { Reminders } from './reminders/reminders';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [Login, Reminders],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  constructor(public auth: AuthService) {}
}
