# (SPRINT_02_PLAN.md)
# Plano da Sprint 2: Vinculação e API de Validação (Simples)

## 1. Objetivo da Sprint

O objetivo desta Sprint é implementar o núcleo do sistema de licenciamento. Vamos modelar os tipos de licença iniciais (Vitalícia e Por Tempo) e permitir que um Administrador as vincule a um Cliente e um Software. Além disso, vamos construir a primeira versão da API de Validação (RF006)  para que os softwares possam verificar o status dessas licenças simples.

## 2. Histórias de Usuário (Backlog da Sprint)

* **US03:** Como Administrador, quero vincular uma Licença (Vitalícia ou Por Tempo) a um Cliente e um Software, para conceder acesso básico.
* **US05:** Como um Software (Ator), quero validar minha licença (Vitalícia/Tempo) via API, para confirmar se posso executar.

## 3. Tarefas Técnicas Detalhadas

### Tarefa 1: Camada de Domínio (`Egide.Domain`)
* [x] Modelar a entidade `Licenca` como uma classe base (abstrata), contendo `Id`, `ClienteId`, `SoftwareId`, `Ativa` e um método abstrato `Validar()`.
* [x] Criar as classes de licença concretas que herdam de `Licenca`:
    * `LicencaVitalicia`
    * `LicencaPorTempo` (com a propriedade `DataExpiracao`)
* [x] Definir a interface `ILicencaRepository` (Abstração do DIP).

### Tarefa 2: Camada de Infraestrutura (`Egide.Infrastructure`)
* [x] Criar uma nova *migration* (`Migration_..._CreateLicencaTable`) para a tabela `licencas`.
    * A tabela deve incluir uma coluna "Tipo" (ex: int) para sabermos qual tipo de licença é (para o Dapper).
    * Deve incluir colunas que possam ser nulas (ex: `DataExpiracao`).
* [x] Implementar a `Persistence/Repositories/LicencaRepository` (implementando `ILicencaRepository` com Dapper).

### Tarefa 3: Camada de Aplicação (`Egide.Application`)
* [x] Implementar os Casos de Uso (Commands) para vincular licenças (US03):
    * `Commands/VincularLicencaVitaliciaCommand` (+Handler e Validator).
    * `Commands/VincularLicencaPorTempoCommand` (+Handler e Validator).
* [x] Implementar o Caso de Uso (Query) para a validação (US05):
    * `Queries/ValidarLicencaQuery` (recebe ClienteId/SoftwareId).
    * `Queries/ValidarLicencaQueryHandler` (busca a licença, chama o método `licenca.Validar()` do domínio e retorna um DTO de resposta, ex: `ValidacaoLicencaResponse`).

### Tarefa 4: Camada de Apresentação (`Egide.Presentation.Api`)
* [x] Registar o novo `ILicencaRepository` no `Program.cs`.
* [x] Criar um novo `LicencasController`:
    * `POST /api/v1/licencas/vincular-vitalicia` (para US03)
    * `POST /api/v1/licencas/vincular-tempo` (para US03)
* [x] Criar um novo `ValidacaoController` (para a API externa, US05):
    * `POST /api/v1/validacao/check` (ou similar, conforme `API_INTEGRATION.md`)

### Tarefa 5: Testes (`Egide.Domain.UnitTests`)
* [x] Escrever testes unitários para a lógica de validação das novas entidades `LicencaVitalicia` e `LicencaPorTempo`.