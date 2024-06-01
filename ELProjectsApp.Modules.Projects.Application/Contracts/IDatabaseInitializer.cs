namespace ELProjectsApp.Modules.Projects.Application.Contracts;
public interface IDatabaseInitializer
{
    Task InitializeDatabase(string databaseName);
}
