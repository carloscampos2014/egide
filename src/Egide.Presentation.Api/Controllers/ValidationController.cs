using Egide.Application.UseCases.Licencas.Queries.GetById;
using Egide.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Egide.Presentation.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ValidationController : ControllerBase
{
    protected readonly IMediator _mediator;

    public ValidationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{id:guid}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResultadoValidacao), StatusCodes.Status200OK)]
    public async Task<IActionResult> Validar(Guid id, [FromBody] ValidationContext context)
    {
        var query = new GetLicencaByIdQuery(id);
        var licenca = await _mediator.Send(query);
        if (licenca == null)
        {
            return Ok(new ResultadoValidacao(false, $"Licença {id} não encontrada"));
        }

        bool valida = licenca.Validar(context);
        string textoValidação = "Esta licença esta valida";
        if (!valida)
        {
            textoValidação = licenca.TextoValidacao(context);
        }

        return Ok(new ResultadoValidacao(valida, textoValidação));
    }
}
