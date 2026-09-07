using RemindersApi.Contracts;
using RemindersApi.Model;

namespace RemindersApi.Apis;

public static class ReminderApis
{
    public static void MapReminderApis(this WebApplication app)
    {
        var group = app.MapGroup("/reminders").RequireAuthorization();

        group.MapGet("/", async (IReminderService svc) =>
            Results.Ok(await svc.GetAllAsync()));

        group.MapGet("/history", async (IReminderService svc) =>
            Results.Ok(await svc.GetHistoryAsync()));

        group.MapPost("/", async (ReminderRequest req, IReminderService svc) =>
        {
            var errors = Validate(req);
            if (errors is not null) return Results.ValidationProblem(errors);
            var created = await svc.CreateAsync(req);
            return Results.Created($"/reminders/{created.Id}", created);
        }).RequireAuthorization("AdminOnly");

        group.MapPut("/{id:int}", async (int id, ReminderRequest req, IReminderService svc) =>
        {
            var errors = Validate(req);
            if (errors is not null) return Results.ValidationProblem(errors);
            var updated = await svc.UpdateAsync(id, req);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        }).RequireAuthorization("AdminOnly");
    }

    private static Dictionary<string, string[]>? Validate(ReminderRequest req)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(req.Name))
            errors[nameof(req.Name)] = ["שם התזכורת הוא שדה חובה"];

        if (req.ScheduledAt == default)
            errors[nameof(req.ScheduledAt)] = ["יש לבחור תאריך ושעה לתזכורת"];

        if (req.FutureRunsCount < 0)
            errors[nameof(req.FutureRunsCount)] = ["מספר הרצות עתידיות לא יכול להיות שלילי"];

        return errors.Count > 0 ? errors : null;
    }
}
