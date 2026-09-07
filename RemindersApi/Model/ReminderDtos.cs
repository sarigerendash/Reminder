namespace RemindersApi.Model;

public record ReminderRequest(
    string Name,
    string Message,
    DateTime ScheduledAt,
    Frequency Frequency,
    bool IsActive,
    int FutureRunsCount
);

public record ReminderResponse(
    int Id,
    string Name,
    string Message,
    DateTime ScheduledAt,
    Frequency Frequency,
    bool IsActive,
    int FutureRunsCount,
    ReminderStatus Status,
    DateTime CreatedAt
);

public record ReminderRunResponse(
    int Id,
    int ReminderId,
    string ReminderName,
    string Message,
    DateTime StartedAt,
    DateTime FinishedAt,
    ReminderStatus Status
);
