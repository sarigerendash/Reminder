import { Component, OnInit, signal, inject, DestroyRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { DatePipe, NgClass } from '@angular/common';
import { interval, startWith, switchMap } from 'rxjs';
import { ReminderService } from '../reminder';
import { AuthService } from '../auth';
import { ReminderForm } from '../reminder-form/reminder-form';
import { Reminder } from '../models';

const REFRESH_INTERVAL_MS = 3000;

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
  private destroyRef = inject(DestroyRef);

  constructor(public svc: ReminderService, public auth: AuthService) {}

  ngOnInit() {
    interval(REFRESH_INTERVAL_MS).pipe(
      startWith(0),
      switchMap(() => this.svc.loadAll()),
      takeUntilDestroyed(this.destroyRef)
    ).subscribe();
  }

  openCreate() { this.editing.set(null); this.showForm.set(true); }
  openEdit(r: Reminder) { this.editing.set(r); this.showForm.set(true); }
  closeForm() { this.showForm.set(false); }

  statusClass(s: string) {
    return { pending: s === 'Pending', running: s === 'Running', success: s === 'Success', failed: s === 'Failed' };
  }
}
