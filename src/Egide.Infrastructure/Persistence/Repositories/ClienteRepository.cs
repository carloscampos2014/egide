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
        string sql = @"INSERT INTO Clientes(Id, Nome, Personalidade, Documento, Ativo, DataCriacao)
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
        string sql = @"DELETE FROM Clientes WHERE Id = @Id";

        await connection.ExecuteAsync(sql, new { Id = id }, transaction: GetTransaction);
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        var connection = GetConnection;
        string sql = "SELECT Id, Nome, Personalidade, Documento, Ativo, DataCriacao FROM Clientes";

        return await connection.QueryAsync<Cliente>(sql, transaction: GetTransaction);
    }

    public async Task<Cliente> GetByIdAsync(Guid id)
    {
        var connection = GetConnection;
        string sql = "SELECT Id, Nome, Personalidade, Documento, Ativo, DataCriacao FROM Clientes WHERE Id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { Id = id }, transaction: GetTransaction);
    }

    public async Task UpdateAsync(Cliente cliente)
    {
        var connection = GetConnection;
        string sql = @"UPDATE Clientes SET 
                          Nome = @Nome, 
                          Personalidade = @Personalidade,
                          Documento = @Documento,
                          Ativo = @Ativo
                       WHERE Id = @Id";


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
