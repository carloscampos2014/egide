using Dapper;
using Egide.Application.Abstractions;
using Egide.Domain.Entities;
using Egide.Domain.Interfaces;

namespace Egide.Infrastructure.Persistence.Repositories;
public class SoftwareRepository : BaseRepository, ISoftwareRepository
{
    public SoftwareRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task AddAsync(Software software)
    {
        var connection = GetConnection;
        string sql = @"INSERT INTO softwares(id, titulo, descricao, versaoatual, ativo, datacriacao)
                       VALUES(@Id, @Titulo, @Descricao, @VersaoAtual, @Ativo, @DataCriacao)";

        await connection.ExecuteAsync(sql, new
        {
            software.Id,
            software.Titulo,
            software.Descricao,
            software.VersaoAtual,
            software.Ativo,
            software.DataCriacao,
        }, transaction: GetTransaction);
    }

    public async Task DeleteAsync(Guid id)
    {
        var connection = GetConnection;
        string sql = @"DELETE FROM softwares WHERE id = @Id";

        await connection.ExecuteAsync(sql, new { Id = id }, transaction: GetTransaction);
    }

    public async Task<IEnumerable<Software>> GetAllAsync()
    {
        var connection = GetConnection;
        string sql = "SELECT id, titulo, descricao, versaoatual, ativo, datacriacao FROM softwares";

        return await connection.QueryAsync<Software>(sql, transaction: GetTransaction);
    }

    public async Task<Software> GetByIdAsync(Guid id)
    {
        var connection = GetConnection;
        string sql = "SELECT id, titulo, descricao, versaoatual, ativo, datacriacao FROM softwares WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Software>(sql, new { Id = id }, transaction: GetTransaction);
    }

    public async Task UpdateAsync(Software software)
    {
        var connection = GetConnection;
        string sql = @"UPDATE softwares SET 
                          titulo = @Titulo, 
                          descricao = @Descricao,
                          versaoatual = @VersaoAtual,
                          ativo = @Ativo
                       WHERE id = @Id";


        await connection.ExecuteAsync(sql, new
        {
            software.Titulo,
            software.Descricao,
            software.VersaoAtual,
            software.Ativo,
            software.Id,
        }, transaction: GetTransaction);
    }
}
