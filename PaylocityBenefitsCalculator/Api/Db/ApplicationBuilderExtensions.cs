namespace Api.Db;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// The method applies all needed database migrations, in our case - data seeding.
    /// </summary>
    public static async Task<WebApplication> EnsureDatabaseCreated(this WebApplication app)
    {
        var scope = app.Services.CreateAsyncScope();
        await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.EnsureCreatedAsync();
        return app;
    }
}