using Egide.Domain.Enums;
using MediatR;

namespace Egide.Application.UseCases.Clientes.Commands.Create;
/// <summary>
/// O 'Command' (Comando) que representa a intenção de criar um novo cliente.
/// Esta classe é um DTO (Data Transfer Object) que carrega os dados para o Handler.
/// Implementa IRequest<Guid> que significa "Eu sou um pedido que espera um 'Guid' como resposta".
/// </summary>
public class CreateClienteCommand : IRequest<Guid>
{
    public string Nome { get; set; }
    
    public Personalidade Personalidade { get; set; } 
 
    public string Documento { get; set; }
}