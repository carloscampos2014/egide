using Egide.Domain.Entities;
using Egide.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace Egide.Application.UseCases.Clientes.Commands.Create;
/// <summary>
/// Validador para o CreateClienteCommand.
/// Garante que os dados de entrada cumprem as regras de negócio básicas.
/// </summary>
/// <summary>
/// O 'Handler' (Manipulador) para o CreateClienteCommand.
/// Esta classe implementa o IRequestHandler e contém a lógica de negócio real.
/// Ela adere ao Princípio da Responsabilidade Única (SRP) [cite: 31-37].
/// </summary>
public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, Guid>
{
    private readonly IClienteRepository _clienteRepository;
    // Futuramente, também injetaremos o IUnitOfWork aqui.

    // Injetamos a "Abstração" (IClienteRepository) e não a "Implementação" (Dapper)
    public CreateClienteCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<Guid> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        // 1. Criamos a entidade de Domínio usando os dados do comando
        var cliente = new Cliente(
            request.Nome,
            request.Personalidade,
            request.Documento
        );

        // 2. Usamos o repositório para persistir a entidade
        await _clienteRepository.AddAsync(cliente);

        // 3. (Futuro: aqui chamaríamos _unitOfWork.SaveChangesAsync())

        // 4. Retornamos o Id da nova entidade
        return cliente.Id;
    }
}