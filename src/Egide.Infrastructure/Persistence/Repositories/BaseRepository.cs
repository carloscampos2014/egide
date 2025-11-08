using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Egide.Infrastructure.Persistence.Repositories;
public abstract class BaseRepository
{
    protected readonly string _connectionString;

    protected BaseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("EgideDb");
    }

    protected NpgsqlConnection GetConnection() => new NpgsqlConnection(_connectionString);
}
