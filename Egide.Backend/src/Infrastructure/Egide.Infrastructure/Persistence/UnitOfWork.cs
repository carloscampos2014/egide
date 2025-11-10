using Egide.Application.Abstractions;
using System.Data.Common;

namespace Egide.Infrastructure.Persistence;
public sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DbConnection _connection;
    private DbTransaction? _transaction;

    public UnitOfWork(IDbConnectionFactory connectionFactory)
    {
        _connection = connectionFactory.CreateConnection(); 
        _transaction = _connection.BeginTransaction();
    }

    public DbTransaction Transaction => _transaction!;

    public void Dispose()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
        _connection.Dispose();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_transaction is null)
                throw new InvalidOperationException("Transação já foi commitada ou não foi iniciada.");

            await _transaction.CommitAsync(cancellationToken);
            return 1; 
        }
        catch
        {
            if (_transaction is not null)
                await _transaction.RollbackAsync(cancellationToken);
            throw; 
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }
}