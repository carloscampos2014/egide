using Egide.Application.Abstractions;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Clientes.Commands.Delete;
public class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommand, Unit>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClienteCommandHandler(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
    {
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.GetByIdAsync(request.Id);
        if (cliente == null)
        {
            throw new InvalidOperationException($"Cliente com Id {request.Id} não encontrado.");
        }

        await _clienteRepository.DeleteAsync(request.Id);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
