using Egide.Application.Abstractions;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Clientes.Commands.Update;
public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, Unit>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClienteCommandHandler(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
    {
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.GetByIdAsync(request.Id);
        if (cliente == null)
        {
            throw new InvalidOperationException($"Cliente com Id {request.Id} não encontrado.");
        }

        cliente.AtualizarDados(
            nome: request.Nome,
            personalidade: request.Personalidade,
            documento: request.Documento);

        await _clienteRepository.UpdateAsync(cliente);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
