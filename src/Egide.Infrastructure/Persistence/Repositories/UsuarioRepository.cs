using Dapper;
using Egide.Application.Abstractions;
using Egide.Domain.Entities;
using Egide.Domain.Interfaces;

namespace Egide.Infrastructure.Persistence.Repositories;
public class UsuarioRepository : BaseRepository, IUsuarioRepository
{
    public UsuarioRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task AddAsync(Usuario usuario)
    {
        var connection = GetConnection;
        string sql = @"INSERT INTO usuarios(id, nome, email, passwordhash, ativo, datacriacao)
                       VALUES(@Id, @Nome, @Email, @PasswordHash, @Ativo, @DataCriacao)";

        await connection.ExecuteAsync(sql, new
        {
            usuario.Id,
            usuario.Nome,
            usuario.Email,
            usuario.PasswordHash,
            usuario.Ativo,
            usuario.DataCriacao,
        }, transaction: GetTransaction);
    }

    public async Task DeleteAsync(Guid id)
    {
        var connection = GetConnection;
        string sql = @"DELETE FROM usuarios WHERE id = @Id";

        await connection.ExecuteAsync(sql, new { Id = id }, transaction: GetTransaction);
    }

    public async Task<Usuario> GetByIdAsync(Guid id)
    {
        var connection = GetConnection;
        string sql = "SELECT id, nome, email, passwordhash, ativo, datacriacao FROM usuario WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id }, transaction: GetTransaction);
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        var connection = GetConnection;
        string sql = @"UPDATE usuarios SET 
                            nome = @Nome,
                            email = @Email,
                            passwordhash = @PasswordHash,
                            ativo = @Ativo
                       WHERE id = @Id";

        await connection.ExecuteAsync(sql, new
        {
            usuario.Nome,
            usuario.Email,
            usuario.PasswordHash,
            usuario.Ativo,
            usuario.Id,
        }, transaction: GetTransaction);
    }
}
