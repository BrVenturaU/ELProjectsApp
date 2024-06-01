# ELProjectsApp
A multitenancy application with physical isolation of tenant information designed in a modular way.


## Running migrations
In order to be able of running migrations, you can do it with the `dotnet ef` command tools or with the Packages Manager Console.

### Using EF Core Tools
If you are using the EF Core command Tools you need to open a console or navigate to the root directory of the project ELProjectsApp at the same level of the `.sln` file, also you can specify the solution on the command line, but placing the command line in the root directory helps avoid errors when specifying those paths when execution commands.

The next commands need to be run:

- Security Schema: Contains users tables and related ones. 
    ```
    dotnet ef database update --context UsersDbContext --project .\ELProjectsApp.Modules.Users.Infrastructure\ELProjectsApp.Modules.Users.Infrastructure.csproj --startup-project .\ELProjectsApp.WebApi\ELProjectsApp.WebApi.csproj
    ```
- Business tables: Contains organization table.
    ```
    dotnet ef database update --context OrganizationsDbContext --project .\ELProjectsApp.Modules.Organizations.Infrastructure\ELProjectsApp.Modules.Organizations.Infrastructure.csproj --startup-project .\ELProjectsApp.WebApi\ELProjectsApp.WebApi.csproj
    ```

It does not matter what command runs first, the first one always creates the database if it does not exist, the second one just adds the schema to the already created database.

### Packages Manager Console
If you are using the Packages Manager Console, you can open the console in Visual Studio IDE and use the Tools commands to run migrations. To open the Package Manager Console go to the IDE menu `Tools > Nuget Packages Manager > Packages Manager Console` this will open the Packages Manager Console and here you can run the commands to run migrations, just chose the **Default Project** as `ELProjectsApp.Modules.Users.Infrastructure` or `ELProjectsApp.Modules.Organizations.Infrastructure` and run the respective commands:
- Security Schema: Contains users tables and related ones. 
    ```
    Update-Database -Context UsersDbContext
    ```
- Business tables: Contains organization table.
    ```
    Update-Database -Context OrganizationsDbContext
    ```

***IMPORTANT***: Tenants *Projects* databases are created ad hoc during the organization creation flow, the migrations related to this module is just for this purpose, if you execute these migrations with the context, it won't fail, but creates a default database, for testing purposes this is fine, but be careful.

## Next steps
- Unified response object.
- Token refresh and session invalidation.
- Fluent Validations.
- Docker.
- Architecture documentation.
- Testing:
    - Unit tests.
    - Integration tests.
    - Architecture tests (Architecture enforcement).
    - TDD practices.
- Domain Driven Design Practices.