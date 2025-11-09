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
            Tipo = (short)licenca.Tipo,
            licenca.DataExpiracao,
            licenca.MaximoUsuarios,
            licenca.MaximoInstalacoes,
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

        return licencas;
    }

    public async Task<Licenca> GetByIdAsync(Guid id)
    {
        var connection = GetConnection;
        string sql = "SELECT id, clienteid, softwareid, tipo, dataexpiracao, maximousuarios, maximoinstalacoes, ativa, datacriacao FROM licencas WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Licenca>(sql, new { Id = id }, transaction: GetTransaction);
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
            Tipo = (short)licenca.Tipo,
            licenca.DataExpiracao,
            licenca.MaximoUsuarios,
            licenca.MaximoInstalacoes,
            licenca.Id,
        }, transaction: GetTransaction);
    }
}
