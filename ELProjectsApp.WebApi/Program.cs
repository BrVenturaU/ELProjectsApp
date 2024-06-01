
using ELProjectsApp.Modules.Organizations.Infrastructure;
using ELProjectsApp.Modules.Projects.Infrastructure;
using ELProjectsApp.Modules.Users.Infrastructure;
using ELProjectsApp.Shared.Infrastructure;
using ELProjectsApp.WebApi.Extensions;
using ELProjectsApp.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .Build();

    options.Filters.Add(new AuthorizeFilter(policy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(builder.Configuration);
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddFilters();
builder.AddAppModule(new SharedInfrastructureModule());
builder.AddAppModule(new UsersModule());
builder.AddAppModule(new OrganizationsModule());
builder.AddAppModule(new ProjectsModule());
builder.Services.AddExceptionHandler<GlobalErrorHandling>();

var app = builder.Build();
app.UseExceptionHandler(_ => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
