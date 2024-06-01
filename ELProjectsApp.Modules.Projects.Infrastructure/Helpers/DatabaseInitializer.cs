using ELProjectsApp.Modules.Projects.Application.Contracts;
using ELProjectsApp.Modules.Projects.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ELProjectsApp.Modules.Projects.Infrastructure.Helpers;

internal class DatabaseInitializer(ProjectsDbContext _dbContext, ITenantProvider _tenantProvider) : IDatabaseInitializer
{
    // This does not ensure the databse is initialized properly.
    // could be necesary to add other mechanism like Outbox or verification middleware
    public async Task InitializeDatabase(string databaseName)
    {
        try
        {
            var db = _tenantProvider.GetTenantConnectionString(databaseName);
            _dbContext.Database.SetConnectionString(db);
            if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Applying ApplicationDB Migrations for New tenant.");
                Console.ResetColor();
                await _dbContext.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
