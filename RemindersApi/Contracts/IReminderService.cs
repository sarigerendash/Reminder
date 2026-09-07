using RemindersApi.Model;

namespace RemindersApi.Contracts;

public interface IReminderService
{
    Task<IEnumerable<ReminderResponse>> GetAllAsync();
    Task<ReminderResponse> CreateAsync(ReminderRequest request);
    Task<ReminderResponse?> UpdateAsync(int id, ReminderRequest request);
    Task<IEnumerable<ReminderRunResponse>> GetHistoryAsync();
}
