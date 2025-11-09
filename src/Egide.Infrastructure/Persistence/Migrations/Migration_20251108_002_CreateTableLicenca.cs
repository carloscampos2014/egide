using FluentMigrator;

namespace Egide.Infrastructure.Persistence.Migrations;
[Migration(20251108002)]
public class Migration_20251108_002_CreateTableLicenca : Migration
{
    public override void Down()
    {
        Delete.Table("licencas");
    }

    public override void Up()
    {
        Create.Table("licencas")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("clienteid").AsGuid().NotNullable()
                .ForeignKey("clientes", "id") 
            .WithColumn("softwareid").AsGuid().NotNullable()
                .ForeignKey("softwares", "id") 
            .WithColumn("tipo").AsInt16().NotNullable()
            .WithColumn("dataexpiracao").AsDateTime().Nullable() 
            .WithColumn("maximousuarios").AsInt32().Nullable() 
            .WithColumn("maximoinstalacoes").AsInt32().Nullable() 
            .WithColumn("ativa").AsBoolean().NotNullable()
            .WithColumn("datacriacao").AsDateTime().NotNullable();
    }
}
