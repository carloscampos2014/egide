using MediatR;

namespace Egide.Application.UseCases.Softwares.Commands.Update;
public class UpdateSoftwareCommad : IRequest<Unit>
{

    public Guid Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string VersaoAtual { get; set; } = string.Empty;
}
