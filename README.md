# Égide 🛡️

**Égide: Plataforma de Gerenciamento de Licenças.**

Um escudo digital para proteger seu software. Égide é a plataforma centralizada para gerenciar, distribuir e validar o ciclo de vida de licenças de software em ambientes Web, Desktop e Mobile.

## 🎯 Documentação Principal

A visão completa do projeto, escopo, arquitetura e guias estão disponíveis na pasta `/docs`:

* [📄 **Visão e Requisitos**](./docs/PROJECT_VISION.md) (O que o projeto faz e por quê)
* [🏗️ **Arquitetura do Sistema**](./docs/ARCHITECTURE.md) (Como o projeto é estruturado)
* [🔌 **Manual de Integração (APIs)**](./docs/API_INTEGRATION.md) (Como usar as APIs)
* [👨‍💻 **Guia do Desenvolvedor**](./docs/DEVELOPER_GUIDE.md) (Como contribuir para o projeto)

## 🚀 Status Atual

**Fase 1: O Alicerce (MVP do Back-end)** - Em Desenvolvimento.

## 🛠️ Tecnologias (Tech Stack)

[cite_start]A plataforma Égide utiliza uma stack moderna baseada em .NET [cite: 5-8, 12, 17, 19, 21]:

* **Core:** .NET 8 / C#
* **APIs:** ASP.NET Core
* **Acesso a Dados:** Dapper (para performance)
* **Banco de Dados:** PostgreSQL
* **Migrations:** FluentMigrator
* **UI (Web Admin):** Blazor Server
* **UI (Nativa):** .NET MAUI

## 🏛️ Arquitetura

[cite_start]O projeto segue rigorosamente os princípios da **Clean Architecture** [cite: 13-16], garantindo total separação entre as regras de negócio (`Domain`), os casos de uso (`Application`), os detalhes de implementação (`Infrastructure`) e as interfaces de usuário/APIs (`Presentation`).

Isso nos permite ter um núcleo testável, robusto e agnóstico de tecnologia. [cite_start]Para mais detalhes, consulte o [Documento de Arquitetura](./docs/ARCHITECTURE.md) [cite: 13-16].

## 🏁 Como Começar (Getting Started)

[cite_start]Siga os passos abaixo para configurar seu ambiente de desenvolvimento [cite: 25-27].

### Pré-requisitos

1.  **SDK do .NET 8:** [Link para download]
2.  **PostgreSQL:** Recomendamos rodar via Docker.
3.  **IDE:** Visual Studio 2022 ou JetBrains Rider.

### Executando o Projeto

1.  Clone o repositório:
    ```bash
    git clone [URL_DO_SEU_REPO]
    cd egide
    ```

2.  Restaure as dependências (a partir da raiz):
    ```bash
    dotnet restore
    ```

3.  Configure sua `appsettings.Development.json` (no projeto `Egide.Presentation.Api` ou `Egide.Presentation.Web`) com a string de conexão do PostgreSQL.

4.  Execute as migrations do banco de dados (WIP - instrução futura).

5.  Execute a aplicação desejada (API ou Web):
    ```bash
    dotnet run --project src/Presentation/Egide.Presentation.Api
    ```

## 👨‍💻 Fluxo de Desenvolvimento

[cite_start]O projeto utiliza um fluxo baseado em Pull Requests (PRs) [cite: 28-30].

1.  Crie uma *feature branch* a partir da `main` (ex: `feat/RF001-cadastro-cliente`).
2.  Implemente a funcionalidade e os testes unitários.
3.  Garanta que o código segue os padrões do [Guia do Desenvolvedor](./docs/DEVELOPER_GUIDE.md).
4.  Abra um Pull Request contra a `main`.
5.  Aguarde a revisão (Code Review) e a aprovação do pipeline de CI.