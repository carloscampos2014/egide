using Egide.Domain.Entities;
using MediatR;

namespace Egide.Application.UseCases.Softwares.Queries.GetAll;
public class GetAllSoftwareQuery : IRequest<IEnumerable<Software>>
{
}
