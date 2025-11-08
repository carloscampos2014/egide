# (SPRINT_01_PLAN.md)
# Plano da Sprint 1: Fundação e CRUDs Base

## 1. Objetivo da Sprint

[cite_start]O objetivo desta Sprint é construir a fundação da solução "Égide" seguindo a Clean Architecture [cite: 436-497] e implementar os Casos de Uso (CRUDs) para as entidades centrais: `Cliente` e `Software`.

Ao final desta Sprint, a API de Administração deve permitir a criação, consulta, atualização e exclusão de Clientes e Softwares.

## 2. Histórias de Usuário (Backlog da Sprint)

* **US01:** Como Administrador, quero gerenciar (CRUD) os Clientes.
* **US02:** Como Administrador, quero gerenciar (CRUD) os Softwares.

## 3. Tarefas Técnicas Detalhadas

### Tarefa 1: Estrutura da Solução (Setup)
* [x] Criar a solução (`Egide.sln`) e os projetos (`.csproj`) no .NET 8:
    * `src/Core/Egide.Domain`
    * `src/Core/Egide.Application`
    * `src/Infrastructure/Egide.Infrastructure`
    * `src/Presentation/Egide.Presentation.Api`
    * `tests/Egide.Domain.UnitTests`

### Tarefa 2: Camada de Domínio (`Egide.Domain`)
* [cite_start][x] Definir a entidade `Cliente` (ex: `Id`, `Nome`, `Documento`, `Ativo`).
* [cite_start][x] Definir a entidade `Software` (ex: `Id`, `Nome`, `Descricao`, `VersaoAtual`).
* [cite_start][x] Definir as interfaces de repositório (Abstrações do DIP [cite: 70-86]):
    * [cite_start]`IClienteRepository` (com métodos `AddAsync`, `GetByIdAsync`, `UpdateAsync`, `DeleteAsync`).
    * [cite_start]`ISoftwareRepository` (com métodos similares).

### Tarefa 3: Camada de Aplicação (`Egide.Application`)
* [x] Instalar pacotes NuGet (ex: MediatR, FluentValidation).
* [x] Implementar os Casos de Uso de `Cliente` (padrão CQRS leve):
    * [cite_start][x] `Commands/CreateClienteCommand` (DTO de entrada).
    * [cite_start][x] `Commands/CreateClienteCommandHandler` (Lógica de orquestração).
    * [cite_start][x] `Commands/CreateClienteCommandValidator` (Validação de CPF/CNPJ).
    * [ ] `Queries/GetClienteByIdQuery`
    * [ ] `Queries/GetClienteByIdQueryHandler`
    * [ ] (Repetir para `Update`, `Delete` e `GetAll`).
* [ ] Implementar os Casos de Uso de `Software` (similar ao Cliente).
* [cite_start][x] Definir a interface `IUnitOfWork` (para gerenciar transações).

### Tarefa 4: Camada de Infraestrutura (`Egide.Infrastructure`)
* [x] Instalar pacotes NuGet (Dapper, FluentMigrator, Npgsql).
* [ ] Implementar as classes concretas dos repositórios:
    * `Persistence/Repositories/ClienteRepository` (implementando `IClienteRepository` com Dapper).
    * `Persistence/Repositories/SoftwareRepository` (implementando `ISoftwareRepository` com Dapper).
* [ ] Implementar a `Persistence/UnitOfWork` (gerenciando a `NpgsqlConnection` e `NpgsqlTransaction`).
* [ ] Criar as *migrations* iniciais com FluentMigrator:
    * `Migrations/Migration_..._CreateClienteTable`
    * `Migrations/Migration_..._CreateSoftwareTable`

### Tarefa 5: Camada de Apresentação (`Egide.Presentation.Api`)
* [cite_start][ ] Configurar o `Program.cs` para Injeção de Dependência (DI)[cite: 74], registrando os repositórios e handlers (MediatR).
* [cite_start][ ] Configurar a conexão com o PostgreSQL (`appsettings.json`)[cite: 236, 266].
* [ ] Criar o `Controllers/ClientesController` (API REST):
    * `POST /api/v1/clientes` (chama o `CreateClienteCommand`).
    * `GET /api/v1/clientes/{id}` (chama o `GetClienteByIdQuery`).
    * (Repetir para `PUT`, `DELETE`, `GET All`).
* [ ] Criar o `Controllers/SoftwaresController` (similar ao Clientes).

### Tarefa 6: Testes (`Egide.Domain.UnitTests`)
* [ ] Escrever testes unitários para as regras de negócio das entidades `Cliente` e `Software` (se houver, ex: validação de documento).