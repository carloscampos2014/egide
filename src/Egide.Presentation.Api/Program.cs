using Egide.Application.Abstractions;
using Egide.Domain.Interfaces;
using Egide.Infrastructure.Persistence;
using Egide.Infrastructure.Persistence.Repositories;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Configuração da Injeção de Dependência (DI) ---

// Infraestrutura (Singleton)
// Registamos a nossa fábrica de conexão como Singleton (só precisa de uma).
builder.Services.AddSingleton<IDbConnectionFactory, NpgsqlConnectionFactory>();

// Infraestrutura (Scoped)
// Registamos o UnitOfWork como Scoped. Um novo UoW (e uma nova transação)
// será criado para cada requisição HTTP.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositórios (Scoped)
// Os repositórios dependem do IUnitOfWork, por isso também devem ser Scoped.
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ISoftwareRepository, SoftwareRepository>();

// Aplicação (MediatR)
// Procura por todos os Handlers (IRequestHandler) no assembly da
// camada de Aplicação e regista-os automaticamente.
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(IUnitOfWork).Assembly)
);

// Aplicação (FluentValidation)
// Procura por todos os Validadores (AbstractValidator) no assembly
// da camada de Aplicação e regista-os.
builder.Services.AddValidatorsFromAssembly(
    typeof(IUnitOfWork).Assembly,
    ServiceLifetime.Scoped
);

// --- 2. Serviços Padrão da API ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();