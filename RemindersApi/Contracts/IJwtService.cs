using RemindersApi.Model;

namespace RemindersApi.Contracts;

public interface IJwtService
{
    string GenerateToken(User user);
}
