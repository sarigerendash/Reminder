using RemindersApi.DTOs;
using RemindersApi.Services;

namespace RemindersApi.Endpoints;

public static class ReminderEndpoints
{
    public static void MapReminderEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/reminders").RequireAuthorization();

        group.MapGet("/", async (IReminderService svc) =>
            Results.Ok(await svc.GetAllAsync()));

        group.MapGet("/{id:int}", async (int id, IReminderService svc) =>
            await svc.GetByIdAsync(id) is { } r ? Results.Ok(r) : Results.NotFound());

        group.MapPost("/", async (ReminderRequest req, IReminderService svc) =>
        {
            var created = await svc.CreateAsync(req);
            return Results.Created($"/reminders/{created.Id}", created);
        }).RequireAuthorization("AdminOnly");

        group.MapPut("/{id:int}", async (int id, ReminderRequest req, IReminderService svc) =>
        {
            var updated = await svc.UpdateAsync(id, req);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        }).RequireAuthorization("AdminOnly");
    }
}
