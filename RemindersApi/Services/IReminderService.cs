using RemindersApi.DTOs;

namespace RemindersApi.Services;

public interface IReminderService
{
    Task<IEnumerable<ReminderResponse>> GetAllAsync();
    Task<ReminderResponse?> GetByIdAsync(int id);
    Task<ReminderResponse> CreateAsync(ReminderRequest request);
    Task<ReminderResponse?> UpdateAsync(int id, ReminderRequest request);
    Task<IEnumerable<ReminderRunResponse>> GetHistoryAsync();
}
