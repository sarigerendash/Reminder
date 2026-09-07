using Microsoft.EntityFrameworkCore;
using RemindersApi.Data;
using RemindersApi.DTOs;
using RemindersApi.Services;

namespace RemindersApi.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/login", async (LoginRequest req, AppDbContext db, JwtService jwt) =>
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Username == req.Username && u.Password == req.Password);
            if (user is null) return Results.Unauthorized();
            return Results.Ok(new LoginResponse(jwt.GenerateToken(user), user.Role));
        });
    }
}
