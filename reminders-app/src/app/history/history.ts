import { Component, OnInit, inject, DestroyRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { DatePipe, NgClass } from '@angular/common';
import { interval, startWith, switchMap } from 'rxjs';
import { ReminderService } from '../reminder';

const REFRESH_INTERVAL_MS = 3000;

@Component({
  selector: 'app-history',
  standalone: true,
  imports: [DatePipe, NgClass],
  templateUrl: './history.html',
  styleUrl: './history.css'
})
export class ReminderHistory implements OnInit {
  private destroyRef = inject(DestroyRef);

  constructor(public svc: ReminderService) {}

  ngOnInit() {
    interval(REFRESH_INTERVAL_MS).pipe(
      startWith(0),
      switchMap(() => this.svc.loadHistory()),
      takeUntilDestroyed(this.destroyRef)
    ).subscribe();
  }

  statusClass(status: string) {
    return { success: status === 'Success', failed: status === 'Failed' };
  }
}
