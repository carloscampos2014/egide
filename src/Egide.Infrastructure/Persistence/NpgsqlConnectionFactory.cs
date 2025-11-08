using Egide.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Egide.Infrastructure.Persistence;
/// <summary>
/// Implementação concreta da IDbConnectionFactory para PostgreSQL.
/// </summary>
public class NpgsqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public NpgsqlConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("EgideDb");
    }

    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}