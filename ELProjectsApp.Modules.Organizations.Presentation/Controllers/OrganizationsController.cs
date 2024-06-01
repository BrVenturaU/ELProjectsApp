using ELProjectsApp.Modules.Organizations.Application.Organizations;
using ELProjectsApp.Modules.Organizations.Presentation.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ELProjectsApp.Modules.Organizations.Presentation.Controllers;

[ApiController]
[Route("api/organizations")]
[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
public class OrganizationsController: ControllerBase
{
    private readonly ISender _sender;

    public OrganizationsController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Get organizations belonging to the currently user session.
    /// </summary>
    /// <returns>A list of organizations.</returns>
    /// <response code="200">An object with all the organizations belonging to the user.</response>
    [HttpGet]
    public async Task<ActionResult> GetOrganizations()
    {
        var result = await _sender.Send(new GetUserOrganizations.GetUserOrganizationsQuery());
        return Ok(result);
    }

    /// <summary>
    /// Get an user organization by identifier.
    /// </summary>
    /// <param name="id">The organization identifier.</param>
    /// <returns>Organization information if encountered.</returns>
    /// <response code="200">An object related to the organization information.</response>
    /// <response code="404">A message indicating the organization was not found.</response>
    [HttpGet("{id:guid}", Name = "GetOrganizationById")]
    public async Task<ActionResult> GetOrganizationById(Guid id)
    {
        var result = await _sender.Send(new GetUserOrganization.GetUserOrganizationQuery(id));

        if (result.IsFailure)
            return NotFound(result.Error);

       return Ok(result.Value);
    }

    /// <summary>
    /// Create an organization assigned to the current user in session.
    /// </summary>
    /// <param name="creationRequest">Organization information to be added.</param>
    /// <returns>The organization created.</returns>
    /// <response code="201">An object indicating the creation was sucessfully.</response>
    [HttpPost]
    public async Task<ActionResult> CreateOrganization(OrganizationCreationRequest creationRequest)
    {
        var result = await _sender
            .Send(new AddOrganization.AddOrganizationCommand(creationRequest.Name));
        return CreatedAtRoute("GetOrganizationById", new { id = result.Id },result);
    }
}
