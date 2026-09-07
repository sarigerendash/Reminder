namespace RemindersApi.Models;

public class ReminderRun
{
    public int Id { get; set; }
    public int ReminderId { get; set; }
    public Reminder Reminder { get; set; } = null!;
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
    public ReminderStatus Status { get; set; }
}
