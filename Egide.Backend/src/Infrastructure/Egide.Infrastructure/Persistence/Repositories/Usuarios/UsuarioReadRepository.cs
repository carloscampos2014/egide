using Dapper;
using Egide.Application.Abstractions;
using Egide.Domain.Entities;
using Egide.Domain.Interfaces.Usuarios;

namespace Egide.Infrastructure.Persistence.Repositories.Usuarios;

public class UsuarioReadRepository : BaseRepository, IUsuarioReadRepository
{
    public UsuarioReadRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public async Task<Usuario?> ObterPorIdAsync(Guid id)
    {
        var conexao = GetConnection;
        const string sql = "SELECT id, nome, email, hashsenha, tokentenant, estaativo, datacriacao, dataatualizacao FROM usuarios WHERE id = @Id";

        return await conexao.QuerySingleOrDefaultAsync<Usuario>(sql, new { Id = id }, transaction: GetTransaction);
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
    {
        var conexao = GetConnection;
        const string sql = "SELECT id, nome, email, hashsenha, tokentenant, estaativo, datacriacao, dataatualizacao FROM usuarios WHERE email = @Email";

        return await conexao.QuerySingleOrDefaultAsync<Usuario>(sql, new { Email = email }, transaction: GetTransaction);
    }
}