using FluentMigrator;

namespace Egide.Infrastructure.Persistence.Migrations;
[Migration(20251108001)]
public class Migration_20251108_001_CreateTables : Migration
{
    public override void Down()
    {
        Delete.Table("Clientes");
        Delete.Table("Softwares");
    }

    public override void Up()
    {
        Create.Table("Clientes")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Nome").AsString(200).NotNullable().Indexed()
            .WithColumn("Personalidade").AsInt16().NotNullable()
            .WithColumn("Documento").AsString(20).NotNullable().Unique()
            .WithColumn("Ativo").AsBoolean().NotNullable()
            .WithColumn("DataCriacao").AsDateTime().NotNullable();

        Create.Table("Softwares")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Titulo").AsString(200).NotNullable().Unique()
            .WithColumn("Descricao").AsString(500).Nullable()
            .WithColumn("VersaoAtual").AsString(11).NotNullable()
            .WithColumn("Ativo").AsBoolean().NotNullable()
            .WithColumn("DataCriacao").AsDateTime().NotNullable();
    }
}
