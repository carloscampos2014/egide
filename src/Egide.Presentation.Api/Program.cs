using Egide.Application.Abstractions;
using Egide.Domain.Interfaces;
using Egide.Infrastructure.Persistence;
using Egide.Infrastructure.Persistence.Repositories;
using FluentValidation;
using FluentMigrator.Runner; // <-- 1. Importe o Runner
using Egide.Infrastructure.Persistence.Migrations; // <-- 2. Importe as suas Migrations

var builder = WebApplication.CreateBuilder(args);

// --- 1. Configuração da Injeção de Dependência (DI) ---

// Infraestrutura (Singleton)
builder.Services.AddSingleton<IDbConnectionFactory, NpgsqlConnectionFactory>();

// Infraestrutura (Scoped)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositórios (Scoped)
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ISoftwareRepository, SoftwareRepository>();
builder.Services.AddScoped<ILicencaRepository, LicencaRepository>();

// Aplicação (MediatR)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(IUnitOfWork).Assembly)
);

// Aplicação (FluentValidation)
builder.Services.AddValidatorsFromAssembly(
    typeof(IUnitOfWork).Assembly,
    ServiceLifetime.Scoped
);

//
// --- 3. Configuração do FluentMigrator ---
//
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        // Define o provider do banco (PostgreSQL)
        .AddPostgres()
        // Pega a connection string do appsettings.json
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("EgideDb"))
        // Informa em qual Assembly (projeto) estão os seus ficheiros de Migration
        .ScanIn(typeof(Migration_20251108_001_CreateTables).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());


// --- 4. Serviços Padrão da API ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//
// --- 5. Executar as Migrations ---
//
// Antes de a API arrancar, vamos garantir que a base de dados está atualizada.
using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    // Executa todos os métodos Up() das migrations pendentes
    runner.MigrateUp();
}

// --- 6. Configure the HTTP request pipeline ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
