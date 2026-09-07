import { Component, Input, Output, EventEmitter, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Reminder, ReminderRequest, Frequency } from '../models';
import { ReminderService } from '../reminder';

@Component({
  selector: 'app-reminder-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './reminder-form.html',
  styleUrl: './reminder-form.css'
})
export class ReminderForm implements OnInit {
  @Input() reminder: Reminder | null = null;
  @Output() closed = new EventEmitter<void>();

  frequencies: Frequency[] = ['Once', 'Daily', 'Weekly', 'Monthly'];
  error = signal('');

  form: ReminderRequest = {
    name: '', message: '', scheduledAt: '', frequency: 'Once', isActive: true, futureRunsCount: 0
  };

  constructor(private svc: ReminderService) {}

  ngOnInit() {
    if (this.reminder) {
      const { name, message, scheduledAt, frequency, isActive, futureRunsCount } = this.reminder;
      this.form = { name, message, scheduledAt: scheduledAt.slice(0, 16), frequency, isActive, futureRunsCount };
    }
  }

  submit() {
    if (!this.form.name || !this.form.scheduledAt) { this.error.set('שם ותאריך הם שדות חובה'); return; }
    const req = { ...this.form, scheduledAt: new Date(this.form.scheduledAt).toISOString() };
    const action = this.reminder
      ? this.svc.update(this.reminder.id, req)
      : this.svc.create(req);
    action.subscribe({ next: () => this.closed.emit(), error: () => this.error.set('שגיאה בשמירה') });
  }
}
