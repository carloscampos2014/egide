using Egide.Application.UseCases.Licencas.Commands.Create;
using Egide.Application.UseCases.Licencas.Commands.Delete;
using Egide.Application.UseCases.Licencas.Commands.Update;
using Egide.Application.UseCases.Licencas.Queries.GetAll;
using Egide.Application.UseCases.Licencas.Queries.GetById;
using Egide.Domain.Entities;
using Egide.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Egide.Presentation.Api.Controllers;
public class LicencasController : BaseController
{
    public LicencasController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateLicencaCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    [HttpPut("{id:guid}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLicencaCommand command)
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
        var command = new DeleteLicencaCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Licenca), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetLicencaByIdQuery(id);
        var licenca = await _mediator.Send(query);
        return licenca is not null ? Ok(licenca) : NotFound();
    }

    [HttpGet("{filtro:int}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Licenca>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(FiltroLicenca filtro)
    {
        var query = new GetAllLicencaQuery(filtro);
        var licencas = await _mediator.Send(query);
        return Ok(licencas);
    }
}
