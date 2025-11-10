# Projeto: Solução de Licenciamento de Software Online

## 1. Visão Geral

O projeto consiste em criar uma plataforma completa para gerenciamento de licenciamento de software. A solução permitirá que desenvolvedores ou empresas (os "usuários" da plataforma) gerenciem seus próprios clientes, softwares e as licenças que vinculam os dois, com um sistema de verificação online.

## 2. Estrutura da Solução

O sistema será dividido em duas soluções principais:

-   **`Egide.Backend`**: Solução .NET contendo a lógica de negócio, acesso a dados e as APIs.
-   **`Egide.Frontend`**: Solução contendo as aplicações cliente que consumirão o backend.

## 3. Tecnologias (Stack)

-   **Linguagem:** C#
-   **Banco de Dados:** PostgreSQL
-   **Acesso a Dados:** Dapper
-   **Migrations:** FluentMigrator
-   **Validação:** FluentValidation
-   **Padrão de Projeto:** MediatR para o fluxo de CQRS.
-   **Autenticação:** JWT (JSON Web Tokens) para Tenants e possivelmente para os clientes finais.

## 4. Módulos e Funcionalidades

### 4.1. Back-end

#### 4.1.1. Web API

A Web API será o coração do sistema, expondo endpoints para:

-   **Gerenciamento de Usuário (Tenant):**
    -   Alteração de dados pessoais (nome, senha).
    -   Geração de Tenant Token (JWT para acesso à API).
-   **Gerenciamento de Clientes:**
    -   CRUD (Create, Read, Update, Delete) de clientes do usuário.
-   **Gerenciamento de Software:**
    -   CRUD de softwares do usuário.
-   **Gerenciamento de Licenças:**
    -   Vincular um software a um cliente através de uma licença.
    -   Definir o tipo de licença.
-   **Verificação de Licença:**
    -   Endpoint público (ou semi-público) para que os softwares dos clientes possam validar o status de suas licenças.

#### 4.1.2. Console Application (Uso Interno)

Uma aplicação de console para administração e tarefas de suporte, com as seguintes funcionalidades:

-   Criar, excluir, ativar e desativar clientes.
-   Revogar Tenant Tokens.
-   Resetar senhas de usuários.

### 4.2. Tipos de Licença

O sistema deve suportar 4 modelos de licenciamento:

1.  **Vitalícia:** A licença não expira.
2.  **Por Tempo Determinado:** A licença tem uma data de expiração.
3.  **Por Número de Usuários:** A licença é válida para um número máximo de usuários concorrentes ou cadastrados.
4.  **Por Número de Instalações:** A licença é válida para um número máximo de instalações ativas.

### 4.3. Front-end

As aplicações front-end consumirão a Web API do back-end.

-   **WebApp:**
    -   Uma aplicação web responsiva para que os usuários gerenciem seus dados, clientes, softwares e licenças.
-   **DesktopApp:**
    -   Uma aplicação de desktop, preferencialmente multiplataforma (Windows, Linux, Mac), com funcionalidades similares à WebApp.
-   **MobileApp:**
    -   Uma aplicação móvel (Android e iOS) para gerenciamento.

## 5. Próximos Passos

1.  Modelagem do Banco de Dados.
2.  Definição da arquitetura detalhada (Clean Architecture).
3.  Desenvolvimento inicial do Back-end (setup do projeto, migrations, primeiras entidades).
