using ELProjectsApp.Modules.Projects.Domain.Contracts;
using ELProjectsApp.Shared.Abstractions.Messaging;
using ELProjectsApp.Shared.Kernel.Dtos.Projects;

namespace ELProjectsApp.Modules.Projects.Application.Projects;

public class GetProjects
{
    public record GetAllProjectsQuery : IQuery<IEnumerable<ProjectDto>>;
    internal sealed class GetAllProjectsQueryHandler (
        IProjectRepository _projectRepository
    ) : IQueryHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
    {
        public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllProjects();
            return projects.Select(p => new ProjectDto(p.Id, p.Name));
        }
    }
}
