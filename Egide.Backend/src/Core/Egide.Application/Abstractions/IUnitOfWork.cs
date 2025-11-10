using System.Data.Common;

namespace Egide.Application.Abstractions;
/// <summary>
/// Define o contrato para o padrão Unit of Work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// A transação de base de dados ativa.
    /// Os repositórios devem usar esta transação para garantir a atomicidade.
    /// </summary>
    DbTransaction Transaction { get; }

    /// <summary>
    /// Salva (commita) todas as alterações.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}