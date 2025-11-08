using Egide.Domain.Entities;
using Egide.Domain.Interfaces;
using MediatR;

namespace Egide.Application.UseCases.Clientes.Queries.GetAll;
public class GetAllClienteQueryHandler : IRequestHandler<GetAllClienteQuery, IEnumerable<Cliente>>
{
    private readonly IClienteRepository _clienteRepository;

    public GetAllClienteQueryHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<Cliente>> Handle(GetAllClienteQuery request, CancellationToken cancellationToken)
    {
        return await _clienteRepository.GetAllAsync();
    }
}
