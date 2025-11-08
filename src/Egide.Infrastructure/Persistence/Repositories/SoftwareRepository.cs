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
        using var connection = GetConnection;
        string sql = @"INSERT INTO Softwares(Id, Titulo, Descricao, VersaoAtual, Ativo, DataCriacao)
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
        using var connection = GetConnection;
        string sql = @"DELETE FROM Softwares WHERE Id = @Id";

        await connection.ExecuteAsync(sql, new { Id = id }, transaction: GetTransaction);
    }

    public async Task<IEnumerable<Software>> GetAllAsync()
    {
        using var connection = GetConnection;
        string sql = "SELECT Id, Titulo, Descricao, VersaoAtual, Ativo, DataCriacao FROM Softwares";

        return await connection.QueryAsync<Software>(sql);
    }

    public async Task<Software> GetByIdAsync(Guid id)
    {
        using var connection = GetConnection;
        string sql = "SELECT Id, Titulo, Descricao, VersaoAtual, Ativo, DataCriacao FROM Softwares WHERE Id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Software>(sql, new { Id = id }, transaction: GetTransaction);
    }

    public async Task UpdateAsync(Software software)
    {
        using var connection = GetConnection;
        string sql = @"UPDATE Softwares SET 
                          Titulo = @Titulo, 
                          Descricao = @Descricao,
                          VersaoAtual = @VersaoAtual,
                          Ativo = @Ativo
                       WHERE Id = @Id";


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
