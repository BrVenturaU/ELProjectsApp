using ELProjectsApp.Modules.Projects.Domain.Projects;
using ELProjectsApp.Modules.Projects.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ELProjectsApp.Modules.Projects.Infrastructure.Database;

public class ProjectsDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    public ProjectsDbContext(DbContextOptions<ProjectsDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProjectEntityTypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
