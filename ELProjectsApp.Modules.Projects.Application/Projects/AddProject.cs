using ELProjectsApp.Modules.Projects.Domain.Contracts;
using ELProjectsApp.Modules.Projects.Domain.Projects;
using ELProjectsApp.Shared.Abstractions.Data;
using ELProjectsApp.Shared.Abstractions.Messaging;
using ELProjectsApp.Shared.Kernel.Dtos.Projects;
using Microsoft.Extensions.DependencyInjection;

namespace ELProjectsApp.Modules.Projects.Application.Projects;

public class AddProject
{
    public record AddProjectCommand(string Name, string Description, int Duration) : ICommand<ProjectDto>;
    internal sealed class AddProjectCommandHandler(
        IProjectRepository _projectRepository, 
        [FromKeyedServices("ProjectsUnitOfWork")] IUnitOfWork _unitOfWork
    ) : ICommandHandler<AddProjectCommand, ProjectDto>
    {
        public async Task<ProjectDto> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project
            {
                Name = request.Name,
                Description = request.Description,
                Duration = request.Duration,
            };

            _projectRepository.Insert(project);

           await _unitOfWork.SaveChangesAsync(cancellationToken);

           return new ProjectDto(project.Id, project.Name);
            
        }
    }
}
