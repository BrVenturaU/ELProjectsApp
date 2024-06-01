namespace ELProjectsApp.Modules.Organizations.Domain.Organizations;

public class Organization
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SlugTenant { get; set; }
    public Guid UserId { get; set; }
}
