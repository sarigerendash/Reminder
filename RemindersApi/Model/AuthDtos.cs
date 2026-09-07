namespace RemindersApi.Model;

public record LoginRequest(string Username, string Password);
public record LoginResponse(string Token, string Role);
