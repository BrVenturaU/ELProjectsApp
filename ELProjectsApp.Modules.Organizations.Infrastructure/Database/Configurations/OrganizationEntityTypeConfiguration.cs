using ELProjectsApp.Modules.Organizations.Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELProjectsApp.Modules.Organizations.Infrastructure.Database.Configurations
{
    internal class OrganizationEntityTypeConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder
                .HasKey(o =>  o.Id);

            builder
                .Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .Property(o => o.UserId)
                .IsRequired();

            builder.Property(o => o.SlugTenant)
                .IsRequired();
            builder.HasIndex(o => o.SlugTenant).IsUnique();
        }
    }
}
