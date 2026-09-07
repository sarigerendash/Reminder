using Microsoft.EntityFrameworkCore;
using RemindersApi.Data;
using RemindersApi.Models;

namespace RemindersApi.Services;

public class ReminderBackgroundService(IServiceScopeFactory scopeFactory, ILogger<ReminderBackgroundService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(30), ct);
            await ProcessPendingReminders(ct);
        }
    }

    private async Task ProcessPendingReminders(CancellationToken ct)
    {
        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var now = DateTime.UtcNow;
        var pending = await db.Reminders
            .Where(r => r.Status == ReminderStatus.Pending && r.IsActive && r.ScheduledAt <= now)
            .ToListAsync(ct);

        foreach (var reminder in pending)
        {
            var startedAt = DateTime.UtcNow;
            reminder.Status = ReminderStatus.Running;
            await db.SaveChangesAsync(ct);
            logger.LogInformation("Reminder {Id} is Running", reminder.Id);

            await Task.Delay(TimeSpan.FromSeconds(10), ct);

            reminder.Status = Random.Shared.Next(2) == 0 ? ReminderStatus.Success : ReminderStatus.Failed;
            db.ReminderRuns.Add(new ReminderRun
            {
                ReminderId = reminder.Id,
                StartedAt = startedAt,
                FinishedAt = DateTime.UtcNow,
                Status = reminder.Status
            });
            await db.SaveChangesAsync(ct);
            logger.LogInformation("Reminder {Id} finished with {Status}", reminder.Id, reminder.Status);
        }
    }
}
