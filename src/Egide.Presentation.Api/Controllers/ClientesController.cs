using Egide.Application.UseCases.Clientes.Commands.Create;
using Egide.Application.UseCases.Clientes.Commands.Delete;
using Egide.Application.UseCases.Clientes.Commands.Update;
using Egide.Application.UseCases.Clientes.Queries.GetAll;
using Egide.Application.UseCases.Clientes.Queries.GetById;
using Egide.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Egide.Presentation.Api.Controllers;
public class ClientesController : BaseController
{
    public ClientesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateClienteCommand command)
    {
        // 4. Usamos o '_mediator' que veio da classe base
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    [HttpPut("{id:guid}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClienteCommand command)
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
        var command = new DeleteClienteCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetClienteByIdQuery(id);
        var cliente = await _mediator.Send(query);
        return cliente is not null ? Ok(cliente) : NotFound();
    }

    [HttpGet]
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Cliente>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllClienteQuery();
        var clientes = await _mediator.Send(query);
        return Ok(clientes);
    }
}
