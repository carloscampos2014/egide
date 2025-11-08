using Egide.Application.Abstractions;
using System.Data;

namespace Egide.Infrastructure.Persistence.Repositories;
public abstract class BaseRepository
{
    private readonly IUnitOfWork _unitOfWork;

    protected BaseRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    protected IDbTransaction GetTransaction() => _unitOfWork.Transaction;

    protected IDbConnection GetConnection() => _unitOfWork.Transaction.Connection!;
}