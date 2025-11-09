using Egide.Domain.Entities;
using Egide.Domain.Enums;
using MediatR;

namespace Egide.Application.UseCases.Licencas.Queries.GetAll;
public class GetAllLicencaQuery : IRequest<IEnumerable<Licenca>>
{
    public GetAllLicencaQuery(FiltroLicenca filtro)
    {
        Filtro = filtro;
    }

    public FiltroLicenca Filtro { get; set; }
}
