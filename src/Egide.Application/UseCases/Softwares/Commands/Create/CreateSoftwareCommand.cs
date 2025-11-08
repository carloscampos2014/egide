using MediatR;

namespace Egide.Application.UseCases.Softwares.Commands.Create;
public class CreateSoftwareCommand : IRequest<Guid>
{
    public string Titulo { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string VersaoAtual { get; set; } = string.Empty;
}
