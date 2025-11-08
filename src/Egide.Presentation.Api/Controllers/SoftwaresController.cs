using Egide.Application.UseCases.Softwares.Commands.Create;
using Egide.Application.UseCases.Softwares.Commands.Delete;
using Egide.Application.UseCases.Softwares.Commands.Update;
using Egide.Application.UseCases.Softwares.Queries.GetAll;
using Egide.Application.UseCases.Softwares.Queries.GetById;
using Egide.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Egide.Presentation.Api.Controllers;
public class SoftwaresController : BaseController
{
    public SoftwaresController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateSoftwareCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    [HttpPut("{id:guid}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSoftwareCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteSoftwareCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Software), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetSoftwareByIdQuery(id);
        var software = await _mediator.Send(query);
        return software is not null ? Ok(software) : NotFound();
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Software>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllSoftwareQuery();
        var softwares = await _mediator.Send(query);
        return Ok(softwares);
    }
}
