using FluentMigrator;

namespace Egide.Infrastructure.Persistence.Migrations;

/// <summary>
/// Migration para criar a tabela de Usuários.
/// </summary>
[Migration(20251109213500, "Criar tabela de Usuários")]
public class M20251109213500_CriarTabelaUsuarios : Migration
{
    private const string NomeTabela = "usuarios";

    /// <summary>
    /// Aplica a migration, criando a tabela.
    /// </summary>
    public override void Up()
    {
        Create.Table(NomeTabela)
            .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("nome").AsString(100).NotNullable()
            .WithColumn("email").AsString(255).NotNullable().Unique()
            .WithColumn("senhahash").AsString(500).NotNullable()
            .WithColumn("datacriacao").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
            .WithColumn("dataatualizacao").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
    }

    /// <summary>
    /// Reverte a migration, excluindo a tabela.
    /// </summary>
    public override void Down()
    {
        Delete.Table(NomeTabela);
    }
}