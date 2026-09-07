using Microsoft.EntityFrameworkCore;
using RemindersApi.Contracts;
using RemindersApi.Data;
using RemindersApi.Model;

namespace RemindersApi.Services;

public class ReminderService(AppDbContext db) : IReminderService
{
    public async Task<IEnumerable<ReminderResponse>> GetAllAsync() =>
        await db.Reminders.Select(r => ToResponse(r)).ToListAsync();

    public async Task<ReminderResponse> CreateAsync(ReminderRequest req)
    {
        var reminder = new Reminder
        {
            Name = req.Name,
            Message = req.Message,
            ScheduledAt = req.ScheduledAt,
            Frequency = req.Frequency,
            IsActive = req.IsActive,
            FutureRunsCount = req.FutureRunsCount
        };
        db.Reminders.Add(reminder);
        await db.SaveChangesAsync();
        return ToResponse(reminder);
    }

    public async Task<ReminderResponse?> UpdateAsync(int id, ReminderRequest req)
    {
        var reminder = await db.Reminders.FindAsync(id);
        if (reminder is null) return null;
        reminder.Name = req.Name;
        reminder.Message = req.Message;
        reminder.ScheduledAt = req.ScheduledAt;
        reminder.Frequency = req.Frequency;
        reminder.IsActive = req.IsActive;
        reminder.FutureRunsCount = req.FutureRunsCount;
        await db.SaveChangesAsync();
        return ToResponse(reminder);
    }

    public async Task<IEnumerable<ReminderRunResponse>> GetHistoryAsync() =>
        await db.ReminderRuns
            .OrderByDescending(h => h.FinishedAt)
            .Select(h => new ReminderRunResponse(
                h.Id, h.ReminderId, h.Reminder.Name, h.Reminder.Message, h.StartedAt, h.FinishedAt, h.Status))
            .ToListAsync();

    private static ReminderResponse ToResponse(Reminder r) =>
        new(r.Id, r.Name, r.Message, r.ScheduledAt, r.Frequency, r.IsActive, r.FutureRunsCount, r.Status, r.CreatedAt);
}
