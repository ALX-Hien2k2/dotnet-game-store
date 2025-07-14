using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrationDb(this WebApplication app)
    {
        // Manually create a scope
        using var scope = app.Services.CreateScope();

        // Create dbContext require a scope
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        // Perform migration
        dbContext.Database.Migrate();
    }
}
