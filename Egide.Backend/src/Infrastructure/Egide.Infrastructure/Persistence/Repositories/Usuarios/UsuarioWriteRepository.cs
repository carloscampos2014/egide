using Dapper;
using Egide.Application.Abstractions;
using Egide.Domain.Entities;
using Egide.Domain.Interfaces.Usuarios;

namespace Egide.Infrastructure.Persistence.Repositories.Usuarios;

public class UsuarioWriteRepository : BaseRepository, IUsuarioWriteRepository
{
    public UsuarioWriteRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public async Task AdicionarAsync(Usuario usuario)
    {
        var conexao = GetConnection;
        const string sql = @"
            INSERT INTO usuarios (id, nome, email, hashsenha, tokentenant, estaativo, datacriacao, dataatualizacao)
            VALUES (@Id, @Nome, @Email, @HashSenha, @TokenTenant, @EstaAtivo, @DataCriacao, @DataAtualizacao)
            ";

        await conexao.ExecuteAsync(sql, usuario, transaction: GetTransaction);
    }

    public async Task AtualizarAsync(Usuario usuario)
    {
        var conexao = GetConnection;
        const string sql = @"
            UPDATE usuarios SET
                nome = @Nome,
                email = @Email,
                hashsenha = @HashSenha,
                tokentenant = @TokenTenant,
                estaativo = @EstaAtivo,
                dataatualizacao = @DataAtualizacao
            WHERE id = @Id
            ";

        await conexao.ExecuteAsync(sql, usuario, transaction: GetTransaction);
    }

    public async Task RemoverAsync(Guid id)
    {
        var conexao = GetConnection;
        const string sql = "DELETE FROM usuarios WHERE id = @Id";

        await conexao.ExecuteAsync(sql, new { Id = id }, transaction: GetTransaction);
    }
}