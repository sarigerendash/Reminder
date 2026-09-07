import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  username = signal('');
  password = signal('');
  error = signal('');

  constructor(private auth: AuthService) {}

  submit() {
    this.auth.login(this.username(), this.password()).subscribe({
      error: () => this.error.set('שם משתמש או סיסמה שגויים')
    });
  }
}
