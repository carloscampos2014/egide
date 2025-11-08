# (SPRINT_01_PLAN.md)
# Plano da Sprint 1: Fundação e CRUDs Base

## 1. Objetivo da Sprint

[cite_start]O objetivo desta Sprint é construir a fundação da solução "Égide" seguindo a Clean Architecture [cite: 436-497] e implementar os Casos de Uso (CRUDs) para as entidades centrais: `Cliente` e `Software`.

Ao final desta Sprint, a API de Administração deve permitir a criação, consulta, atualização e exclusão de Clientes e Softwares.

## 2. Histórias de Usuário (Backlog da Sprint)

* **US01:** Como Administrador, quero gerenciar (CRUD) os Clientes, para saber para quem estamos licenciando o software.
* **US02:** Como Administrador, quero gerenciar (CRUD) os Softwares, para identificar quais produtos estão sujeitos a licenciamento.

## 3. Tarefas Técnicas Detalhadas

As tarefas estão divididas por camada da arquitetura, conforme nosso `ARCHITECTURE.md`.

### Tarefa 1: Estrutura da Solução (Setup)
* [ ] Criar a solução (`Egide.sln`) e os projetos (`.csproj`) no .NET 8:
    * `src/Core/Egide.Domain`
    * `src/Core/Egide.Application`
    * `src/Infrastructure/Egide.Infrastructure`
    * `src/Presentation/Egide.Presentation.Api`
    * `tests/Egide.Domain.UnitTests`

### Tarefa 2: Camada de Domínio (`Egide.Domain`)
* [ ] Definir a entidade `Cliente` (ex: `Id`, `Nome`, `Documento`, `Ativo`).
* [ ] Definir a entidade `Software` (ex: `Id`, `Nome`, `Descricao`, `VersaoAtual`).
* [cite_start][ ] Definir as interfaces de repositório (Abstrações do DIP [cite: 70-86]):
    * `IClienteRepository` (com métodos `AddAsync`, `GetByIdAsync`, `UpdateAsync`, `DeleteAsync`).
    * `ISoftwareRepository` (com métodos similares).

### Tarefa 3: Camada de Aplicação (`Egide.Application`)
* [ ] Instalar pacotes NuGet (ex: MediatR, FluentValidation).
* [ ] Implementar os Casos de Uso de `Cliente` (padrão CQRS leve):
    * `Commands/CreateClienteCommand` (DTO de entrada)
    * `Commands/CreateClienteCommandHandler` (Lógica de orquestração)
    * `Queries/GetClienteByIdQuery`
    * `Queries/GetClienteByIdQueryHandler`
    * (Repetir para `Update`, `Delete` e `GetAll`).
* [ ] Implementar os Casos de Uso de `Software` (similar ao Cliente).
* [ ] Definir a interface `IUnitOfWork` (para gerenciar transações).

### Tarefa 4: Camada de Infraestrutura (`Egide.Infrastructure`)
* [ ] Instalar pacotes NuGet (Dapper, FluentMigrator, Npgsql).
* [ ] Implementar as classes concretas dos repositórios:
    * `Persistence/Repositories/ClienteRepository` (implementando `IClienteRepository` com Dapper).
    * `Persistence/Repositories/SoftwareRepository` (implementando `ISoftwareRepository` com Dapper).
* [ ] Implementar a `Persistence/UnitOfWork` (gerenciando a `NpgsqlConnection` e `NpgsqlTransaction`).
* [ ] Criar as *migrations* iniciais com FluentMigrator:
    * `Migrations/Migration_20251107_001_CreateClienteTable`
    * `Migrations/Migration_20251107_002_CreateSoftwareTable`

### Tarefa 5: Camada de Apresentação (`Egide.Presentation.Api`)
* [cite_start][ ] Configurar o `Program.cs` para Injeção de Dependência (DI)[cite: 74], registrando os repositórios e handlers (MediatR).
* [ ] Configurar a conexão com o PostgreSQL (`appsettings.json`).
* [ ] Criar o `Controllers/ClientesController` (API REST):
    * `POST /api/v1/clientes` (chama o `CreateClienteCommand`).
    * `GET /api/v1/clientes/{id}` (chama o `GetClienteByIdQuery`).
    * (Repetir para `PUT`, `DELETE`, `GET All`).
* [ ] Criar o `Controllers/SoftwaresController` (similar ao Clientes).

### Tarefa 6: Testes (`Egide.Domain.UnitTests`)
* [ ] Escrever testes unitários para as regras de negócio das entidades `Cliente` e `Software` (se houver, ex: validação de documento).