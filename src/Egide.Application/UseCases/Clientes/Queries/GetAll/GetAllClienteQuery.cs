using Egide.Domain.Entities;
using MediatR;

namespace Egide.Application.UseCases.Clientes.Queries.GetAll;
public class GetAllClienteQuery : IRequest<IEnumerable<Cliente>>
{
}
