namespace Egide.Application.Abstractions;
/// <summary>
/// Define o contrato para o padrão Unit of Work.
/// Usado para agrupar operações de repositório numa única transação de base de dados.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Salva (commita) todas as alterações feitas no contexto da transação atual.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>O número de linhas afetadas (opcional, dependendo da implementação).</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}