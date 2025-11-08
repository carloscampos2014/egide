# (ARCHITECTURE.md)
# Documentação de Arquitetura - Égide

## 1. Visão Geral

[cite_start]A arquitetura do Égide é baseada na **Clean Architecture**[cite: 8], garantindo a separação de responsabilidades (SoC), testabilidade e independência de frameworks ou banco de dados.

O fluxo de dependência é sempre de fora para dentro: `Presentation` -> `Application` -> `Domain`.

## 2. Camadas do Projeto

[cite_start]A estrutura de solução segue o padrão do Guia de Onboarding Técnico [cite: 12-16]:

### 2.1. `src/Core/Egide.Domain`
* **Propósito:** O núcleo do sistema. Contém as regras de negócio puras.
* **Conteúdo:**
    * Entidades: `Cliente`, `Software`, `Licenca` (abstrata), `LicencaPorTempo`, `LicencaPorUsuario`, etc.
    * Interfaces de Repositório (Ex: `IClienteRepository`).
    * Eventos de Domínio (se necessário).
* **Dependências:** Nenhuma.

### 2.2. `src/Core/Egide.Application`
* **Propósito:** Orquestra os casos de uso (*use cases*) do sistema.
* **Conteúdo:**
    * *Handlers* de Casos de Uso (ex: `VincularLicencaCommandHandler`).
    * DTOs (Data Transfer Objects) e ViewModels.
    * Interfaces de serviços de infraestrutura (ex: `IEmailService`, `IUnitOfWork`).
    * Lógica de validação de entrada (FluentValidation).
* **Dependências:** `Egide.Domain`.

### 2.3. `src/Infrastructure/Egide.Infrastructure`
* **Propósito:** Implementa os detalhes técnicos e "coisas externas".
* **Conteúdo:**
    * Implementação dos Repositórios (`Egide.Domain`) usando **Dapper** e **PostgreSQL**.
    * Gerenciamento de Migrations de banco de dados com **FluentMigrator**.
    * Implementação de serviços (ex: envio de e-mail, logging).
    * A classe `UnitOfWork` que gerencia a transação do banco de dados.
* **Dependências:** `Egide.Application`.

### 2.4. `src/Presentation`
* **Propósito:** Pontos de entrada do sistema. (Interfaces de Usuário e APIs).
* **Conteúdo:**
    * `Egide.Presentation.Api`: A API REST (ASP.NET Core) que expõe os endpoints para administração (interna) e validação (externa).
    * `Egide.Presentation.Web`: O painel de administração web em **Blazor Server**.
    * `Egide.Presentation.Native`: O aplicativo nativo (Desktop/Mobile) em **.NET MAUI**.
* [cite_start]**Dependências:** `Egide.Infrastructure` e `Egide.Application` (via Injeção de Dependência [cite: 35]).
