using Dapper;
using Egide.Application.Abstractions;
using Egide.Domain.Entities;
using Egide.Domain.Interfaces;

namespace Egide.Infrastructure.Persistence.Repositories;
public class ClienteRepository : BaseRepository, IClienteRepository
{
    public ClienteRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task AddAsync(Cliente cliente)
    {
        var connection = GetConnection;
        string sql = @"INSERT INTO clientes(id, nome, personalidade, documento, ativo, datacriacao)
                       VALUES(@Id, @Nome, @Personalidade, @Documento, @Ativo, @DataCriacao)";

        await connection.ExecuteAsync(sql, new
        {
            cliente.Id,
            cliente.Nome,
            Personalidade = (int)cliente.Personalidade,
            cliente.Documento,
            cliente.Ativo,
            cliente.DataCriacao, 
        }, transaction: GetTransaction);
    }

    public async Task DeleteAsync(Guid id)
    {
        var connection = GetConnection;
        string sql = @"DELETE FROM clientes WHERE id = @Id";

        await connection.ExecuteAsync(sql, new { Id = id }, transaction: GetTransaction);
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        var connection = GetConnection;
        string sql = "SELECT id, nome, personalidade, documento, ativo, datacriacao FROM clientes";

        return await connection.QueryAsync<Cliente>(sql, transaction: GetTransaction);
    }

    public async Task<Cliente> GetByIdAsync(Guid id)
    {
        var connection = GetConnection;
        string sql = "SELECT id, nome, personalidade, documento, ativo, datacriacao FROM clientes WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { Id = id }, transaction: GetTransaction);
    }

    public async Task UpdateAsync(Cliente cliente)
    {
        var connection = GetConnection;
        string sql = @"UPDATE clientes SET 
                          nome = @Nome, 
                          personalidade = @Personalidade,
                          documento = @Documento,
                          ativo = @Ativo
                       WHERE id = @Id";


        await connection.ExecuteAsync(sql, new
        {
            cliente.Nome,
            Personalidade = (int)cliente.Personalidade,
            cliente.Documento,
            cliente.Ativo,
            cliente.Id,
        }, transaction: GetTransaction);
    }
}
