import { Component, OnInit, signal } from '@angular/core';
import { DatePipe, NgClass } from '@angular/common';
import { ReminderService } from '../reminder';
import { AuthService } from '../auth';
import { ReminderForm } from '../reminder-form/reminder-form';
import { Reminder } from '../models';

@Component({
  selector: 'app-reminders',
  standalone: true,
  imports: [ReminderForm, DatePipe, NgClass],
  templateUrl: './reminders.html',
  styleUrl: './reminders.css'
})
export class Reminders implements OnInit {
  showForm = signal(false);
  editing = signal<Reminder | null>(null);

  constructor(public svc: ReminderService, public auth: AuthService) {}

  ngOnInit() { this.svc.loadAll().subscribe(); }

  openCreate() { this.editing.set(null); this.showForm.set(true); }
  openEdit(r: Reminder) { this.editing.set(r); this.showForm.set(true); }
  closeForm() { this.showForm.set(false); }

  statusClass(s: string) {
    return { pending: s === 'Pending', running: s === 'Running', success: s === 'Success', failed: s === 'Failed' };
  }
}
