using ELProjectsApp.Modules.Projects.Domain.Contracts;
using ELProjectsApp.Modules.Projects.Domain.Projects;
using ELProjectsApp.Shared.Abstractions.Messaging;
using ELProjectsApp.Shared.Kernel;
using ELProjectsApp.Shared.Kernel.Dtos.Projects;

namespace ELProjectsApp.Modules.Projects.Application.Projects;

public class GetProject
{
    public record GetProjectQuery (Guid Id): IQuery<Result<ProjectDto>>;
    internal sealed class GetProjectQueryHandler(
        IProjectRepository _projectRepository
    ) : IQueryHandler<GetProjectQuery, Result<ProjectDto>>
    {
        public async Task<Result<ProjectDto>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectById(request.Id);
            return 
                project is not null ? 
                new ProjectDto(project.Id, project.Name) :
                ProjectErrors.NotFound; 
        }
    }
}