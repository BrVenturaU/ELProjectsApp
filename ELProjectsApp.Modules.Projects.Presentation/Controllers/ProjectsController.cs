using ELProjectsApp.Modules.Projects.Application.Projects;
using ELProjectsApp.Modules.Projects.Presentation.Filters;
using ELProjectsApp.Modules.Projects.Presentation.Requests;
using ELProjectsApp.Shared.Kernel.Dtos.Projects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ELProjectsApp.Modules.Projects.Presentation.Controllers
{
    [ApiController]
    [Route("/api/{slugTenant:guid}/projects")]
    [ServiceFilter(typeof(TenantAuthorizationFilter))]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    public class ProjectsController: ControllerBase
    {
        private readonly ISender _sender;

        public ProjectsController(ISender sender)
        {
            _sender = sender;
        }
        /// <summary>
        /// Get all the tenant projects.
        /// </summary>
        /// <param name="slugTenant">The tenant slug identifier.</param>
        /// <returns>A list of all projects belonging to the tenant.</returns>
        [HttpGet]
        public async Task<ActionResult<ProjectDto>> GetProjects(Guid slugTenant)
        {
            var result = await _sender.Send(new GetProjects.GetAllProjectsQuery());

            return Ok(result);
        }

        /// <summary>
        /// Get a tenant project by identifier.
        /// </summary>
        /// <param name="id">The project identifier.</param>
        /// <param name="slugTenant">The tenant slug identifier.</param>
        /// <returns>Project information if encountered.</returns>
        /// <response code="200">An object related to the project information.</response>
        /// <response code="404">A message indicating the project was not found.</response>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProjectDto>> GetProject(Guid slugTenant, Guid id)
        {
            var result = await _sender.Send(new GetProject.GetProjectQuery(id));

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        /// <summary>
        /// Create a tenant project.
        /// </summary>
        /// <param name="projectCreation">Project information to be added.</param>
        /// <param name="slugTenant">The tenant slug identifier.</param>
        /// <returns>The project created.</returns>
        /// <response code="201">An object indicating the creation was sucessfully.</response>
        [HttpPost]
        public async Task<ActionResult> CreateProject(Guid slugTenant, ProjectCreationRequest projectCreation)
        {
            var result = await _sender
                .Send(new AddProject.AddProjectCommand(projectCreation.Name, projectCreation.Description, projectCreation.Duration));

            return StatusCode((int)HttpStatusCode.Created, result);
        }
    }
}
