using FluentMigrator;

namespace Egide.Infrastructure.Persistence.Migrations;
[Migration(20251108001)]
public class Migration_20251108_001_CreateTables : Migration
{
    public override void Down()
    {
        Delete.Table("clientes");
        Delete.Table("softwares");
    }

    public override void Up()
    {
        Create.Table("clientes")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("nome").AsString(200).NotNullable().Indexed()
            .WithColumn("personalidade").AsInt16().NotNullable()
            .WithColumn("documento").AsString(20).NotNullable().Unique()
            .WithColumn("ativo").AsBoolean().NotNullable()
            .WithColumn("datacriacao").AsDateTime().NotNullable();

        Create.Table("softwares")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("titulo").AsString(200).NotNullable().Unique()
            .WithColumn("descricao").AsString(500).Nullable()
            .WithColumn("versaoatual").AsString(11).NotNullable()
            .WithColumn("ativo").AsBoolean().NotNullable()
            .WithColumn("datacriacao").AsDateTime().NotNullable();
    }
}
