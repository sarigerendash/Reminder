using RemindersApi.Models;

namespace RemindersApi.DTOs;

public record LoginRequest(string Username, string Password);
public record LoginResponse(string Token, string Role);

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
