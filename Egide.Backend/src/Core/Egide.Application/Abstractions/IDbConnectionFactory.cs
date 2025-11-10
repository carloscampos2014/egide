using System.Data.Common;

namespace Egide.Application.Abstractions;
/// <summary>
/// Abstrai a criação da conexão com o banco de dados.
/// Permite que a infraestrutura seja independente de um provedor de DB (ex: Npgsql).
/// </summary>
public interface IDbConnectionFactory
{
    /// <summary>
    /// Cria e abre uma nova conexão com o banco de dados.
    /// </summary>
    /// <returns>Uma instância de IDbConnection.</returns>
    DbConnection CreateConnection();
}