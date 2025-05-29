using System;
using System.Threading.Tasks;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.PhysicalPerson.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Timelines.Application.Entities.PhysicalPersons.Commands.CreatePhysicalPerson;
using Timelines.Application.Entities.PhysicalPersons.Commands.DeletePhysicalPerson;
using Timelines.Application.Entities.PhysicalPersons.Commands.UpdatePhysicalPerson;
using Timelines.Application.Entities.PhysicalPersons.Queries.GetPhysicalPersonById;
using Timelines.Application.Entities.PhysicalPersons.Queries.ListPhysicalPersons;

namespace Timelines.Api.Controllers.PhysicalPersons;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class PhysicalPersonsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreatePhysicalPersonResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreatePhysicalPersonResponse>> Create([FromBody] CreatePhysicalPersonRequest request)
    {
        var command = request.Adapt<CreatePhysicalPersonCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<CreatePhysicalPersonResponse>();

        return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
    }
    
    [HttpGet("{physicalPersonId}")]
    [ProducesResponseType(typeof(GetPhysicalPersonByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPhysicalPersonByIdResponse>> GetById(
        [FromRoute] string physicalPersonId)
    {
        var result = await sender.Send(new GetPhysicalPersonByIdQuery(physicalPersonId));

        if (result is null)
            return NotFound();

        var response = result.Adapt<GetPhysicalPersonByIdResponse>();

        return Ok(response);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ListPhysicalPersonsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListPhysicalPersonsResponse>> List([FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListPhysicalPersonsQuery(query));
        var response = result.Adapt<ListPhysicalPersonsResponse>();

        return Ok(response);
    }
    
    [HttpPut("{physicalPersonId}")]
    [ProducesResponseType(typeof(UpdatePhysicalPersonResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdatePhysicalPersonResponse>> Update([FromRoute] string physicalPersonId, [FromBody] UpdatePhysicalPersonRequest request)
    {
        var command = new UpdatePhysicalPersonCommand
        {
            Id = PhysicalPersonId.Of(Guid.Parse(physicalPersonId)),
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            ParentName = request.ParentName,
            BirthDate = request.BirthDate,
            StreetAddress = request.StreetAddress,
            PersonalIdNumber = request.PersonalIdNumber,
            IdCardNumber = request.IdCardNumber,
            EmailAddress = request.EmailAddress,
            PhoneNumber = request.PhoneNumber,
            BankAccountNumber = request.BankAccountNumber,
            Comment = request.Comment
        };
        
        var result = await sender.Send(command);
        var response = result.Adapt<UpdatePhysicalPersonResponse>();

        return Ok(response);
    }
    
    [HttpDelete("{physicalPersonId}")]
    [ProducesResponseType(typeof(DeletePhysicalPersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeletePhysicalPersonResponse>> Delete([FromRoute] string physicalPersonId)
    {
        var result = await sender.Send(new DeletePhysicalPersonCommand(physicalPersonId));
        var response = result.Adapt<DeletePhysicalPersonResponse>();

        return Ok(response);
    }
}
