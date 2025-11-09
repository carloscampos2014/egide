using Dapper;
using Egide.Application.Abstractions;
using Egide.Domain.Entities;
using Egide.Domain.Enums;
using Egide.Domain.Interfaces;

namespace Egide.Infrastructure.Persistence.Repositories;
public class LicencaRepository : BaseRepository, ILicencaRepository
{
    public LicencaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task AddAsync(Licenca licenca)
    {
        var connection = GetConnection;
        string sql = @"
            INSERT INTO licencas (id, clienteid, softwareid, tipo, dataexpiracao, maximousuarios, maximoinstalacoes, ativa, datacriacao)
            VALUES (@Id, @ClienteId, @SoftwareId, @Tipo, @DataExpiracao, @MaximoUsuarios, @MaximoInstalacoes, @Ativa, @DataCriacao)";

        await connection.ExecuteAsync(sql, new
        {
            licenca.Id,
            licenca.ClienteId,
            licenca.SoftwareId,
            Tipo = (short)GetTipoLicenca(licenca),
            (licenca as LicencaPorTempo)?.DataExpiracao,
            (licenca as LicencaPorUsuario)?.MaximoUsuarios,
            (licenca as LicencaPorInstalacao)?.MaximoInstalacoes,
            licenca.Ativa,
            licenca.DataCriacao,
        }, transaction: GetTransaction);
    }

    public async Task DeleteAsync(Guid id)
    {
        var connection = GetConnection;
        string sql = @"DELETE FROM licencas WHERE id = @Id";

        await connection.ExecuteAsync(sql, new { Id = id }, transaction: GetTransaction);
    }

    public async Task<IEnumerable<Licenca>> GetAllAsync(FiltroLicenca filtro)
    {
        var connection = GetConnection;
        string sql = "SELECT id, clienteid, softwareid, tipo, dataexpiracao, maximousuarios, maximoinstalacoes, ativa, datacriacao FROM licencas";
        var licencas = await connection.QueryAsync<Licenca>(sql, transaction: GetTransaction);
        if (filtro != FiltroLicenca.Todas)
        {
            bool ativa = filtro == FiltroLicenca.Ativas;
            licencas = licencas.Where(w => w.Ativa == ativa).ToList();
        }

        return licencas.Select(Materialize).ToList();
    }

    public async Task<Licenca> GetByIdAsync(Guid id)
    {
        var connection = GetConnection;
        string sql = "SELECT id, clienteid, softwareid, tipo, dataexpiracao, maximousuarios, maximoinstalacoes, ativa, datacriacao FROM licencas WHERE id = @Id";
        var licencas = await connection.QueryAsync<Licenca>(sql, new { Id = id }, transaction: GetTransaction);

        return licencas.Select(Materialize).FirstOrDefault();
    }

    public async Task UpdateAsync(Licenca licenca)
    {
        var connection = GetConnection;
        string sql = @"
            UPDATE licencas SET 
                 ativa = @Ativa,
                 tipo = @Tipo, 
                 dataexpiracao = @DataExpiracao, 
                 maximousuarios = @MaximoUsuarios, 
                 maximoinstalacoes = @MaximoInstalacoes
            WHERE id = @Id";

        await connection.ExecuteAsync(sql, new
        {
            licenca.Ativa,
            Tipo = (short)GetTipoLicenca(licenca),
            (licenca as LicencaPorTempo)?.DataExpiracao,
            (licenca as LicencaPorUsuario)?.MaximoUsuarios,
            (licenca as LicencaPorInstalacao)?.MaximoInstalacoes,
            licenca.Id,
        }, transaction: GetTransaction);
    }

    private static Licenca Materialize(dynamic row)
    {
        TipoLicenca tipo = (TipoLicenca)row.tipo;
        Guid id = row.id;
        Guid clienteId = row.clienteid;
        Guid softwareId = row.softwareid;
        bool ativa = row.ativo;
        DateTime dataCriacao = row.datacriacao;

        return tipo switch
        {
            TipoLicenca.Vitalicia => new LicencaVitalicia(clienteId, softwareId) { Id = id, Ativa = ativa, DataCriacao = dataCriacao },

            TipoLicenca.PorTempo => new LicencaPorTempo(clienteId, softwareId, row.dataexpiracao) { Id = id, Ativa = ativa, DataCriacao = dataCriacao },

            // Sprints Futuras
            TipoLicenca.PorUsuario => new LicencaPorUsuario(clienteId, softwareId, row.maximousuarios) { Id = id, Ativa = ativa, DataCriacao = dataCriacao },
            TipoLicenca.PorInstalacao => new LicencaPorInstalacao(clienteId, softwareId, row.maximoinstalacoes) { Id = id, Ativa = ativa, DataCriacao = dataCriacao },

            _ => throw new InvalidOperationException($"Tipo de licença desconhecido: {tipo}")
        };
    }

    private static TipoLicenca GetTipoLicenca(Licenca licenca) => licenca switch
    {
        LicencaVitalicia => TipoLicenca.Vitalicia,
        LicencaPorTempo => TipoLicenca.PorTempo,
        LicencaPorUsuario => TipoLicenca.PorUsuario,
        LicencaPorInstalacao => TipoLicenca.PorInstalacao,
        _ => throw new InvalidOperationException("Tipo de licença não mapeado para persistência.")
    };
}
