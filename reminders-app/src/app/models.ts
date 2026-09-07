export type Frequency = 'Once' | 'Daily' | 'Weekly' | 'Monthly';
export type ReminderStatus = 'Pending' | 'Running' | 'Success' | 'Failed';

export interface Reminder {
  id: number;
  name: string;
  message: string;
  scheduledAt: string;
  frequency: Frequency;
  isActive: boolean;
  futureRunsCount: number;
  status: ReminderStatus;
  createdAt: string;
}

export interface ReminderRequest {
  name: string;
  message: string;
  scheduledAt: string;
  frequency: Frequency;
  isActive: boolean;
  futureRunsCount: number;
}
