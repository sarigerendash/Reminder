using Microsoft.EntityFrameworkCore;
using RemindersApi.Contracts;
using RemindersApi.Data;
using RemindersApi.Model;

namespace RemindersApi.Apis;

public static class AuthApis
{
    public static void MapAuthApis(this WebApplication app)
    {
        app.MapPost("/auth/login", async (LoginRequest req, AppDbContext db, IJwtService jwt) =>
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Username == req.Username && u.Password == req.Password);
            if (user is null) return Results.Unauthorized();
            return Results.Ok(new LoginResponse(jwt.GenerateToken(user), user.Role));
        });
    }
}
