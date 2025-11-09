using Egide.Domain.Enums;
using MediatR;

namespace Egide.Application.UseCases.Licencas.Commands.Update;
public class UpdateLicencaCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public TipoLicenca Tipo { get; set; } = TipoLicenca.Vitalicia;

    public DateTime DataExpiracao { get; set; } = DateTime.UtcNow;

    public int MaximoInstalacoes { get; set; } = 1;

    public int MaximoUsuarios { get; set; } = 1;
}
