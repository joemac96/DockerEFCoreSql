using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder MigrateDatabase<T>(this IApplicationBuilder builder) where T : DbContext
        {
            using var scope = builder.ApplicationServices.CreateScope();
            try
            {
                var db = scope.ServiceProvider.GetRequiredService<T>();
                db.Database.Migrate();
            }
            catch(Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }

            return builder;
        }
    }
}
