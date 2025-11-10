using Egide.Application.Abstractions;
using System.Data.Common;

namespace Egide.Infrastructure.Persistence.Repositories;
public abstract class BaseRepository
{
    private readonly IUnitOfWork _unitOfWork;

    protected BaseRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    protected DbTransaction GetTransaction => _unitOfWork.Transaction;

    protected DbConnection GetConnection => _unitOfWork.Transaction.Connection!;
}