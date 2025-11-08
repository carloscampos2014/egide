using Egide.Domain.Enums;
using MediatR;

namespace Egide.Application.UseCases.Clientes.Commands.Update;
public class UpdateClienteCommand : IRequest<Unit>
{
    public Guid Id { get; set; } 

    public string Nome { get; set; } = string.Empty;

    public Personalidade Personalidade { get; set; } = Personalidade.Juridico;

    public string Documento { get; set; } = string.Empty;
}
