using ELProjectsApp.Modules.Projects.Domain.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELProjectsApp.Modules.Projects.Infrastructure.Database.Configurations
{
    internal class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .Property(p => p.Description)
                .HasMaxLength(200);

            builder.Property(p => p.Duration)
                .IsRequired();
        }
    }
}
