using Egide.Domain.Entities;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Clientes.Queries.GetById;
public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, Cliente?>
{
    private readonly IClienteRepository _clienteRepository;

    public GetClienteByIdQueryHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<Cliente?> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
    {
        return await _clienteRepository.GetByIdAsync(request.Id);
    }
}
