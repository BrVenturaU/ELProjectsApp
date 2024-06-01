using ELProjectsApp.Modules.Organizations.Domain.Organizations;
using ELProjectsApp.Modules.Organizations.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ELProjectsApp.Modules.Organizations.Infrastructure.Database;

public class OrganizationsDbContext: DbContext
{
    public DbSet<Organization> Organizations { get; set; }
    public OrganizationsDbContext(DbContextOptions<OrganizationsDbContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrganizationEntityTypeConfiguration());
        modelBuilder.HasDefaultSchema("Business");
        base.OnModelCreating(modelBuilder);
    }
}
