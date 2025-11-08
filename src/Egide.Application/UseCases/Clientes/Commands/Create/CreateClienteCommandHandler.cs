using Egide.Application.Abstractions;
using Egide.Domain.Entities;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Clientes.Commands.Create;
public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, Guid>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClienteCommandHandler(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
    {
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente(
            nome: request.Nome,
            personalidade: request.Personalidade,
            documento: request.Documento
        );

        await _clienteRepository.AddAsync(cliente);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return cliente.Id;
    }
}