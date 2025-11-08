using Egide.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Egide.Infrastructure.Persistence;
public sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly IDbConnection _connection;
    private IDbTransaction? _transaction;

    public UnitOfWork(IDbConnectionFactory connectionFactory)
    {
        _connection = connectionFactory.CreateConnection(); 
        _transaction = _connection.BeginTransaction();
    }

    public IDbTransaction Transaction => _transaction!;

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
