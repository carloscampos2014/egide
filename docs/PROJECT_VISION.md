# (PROJECT_VISION.md)
# Documento de Visão e Requisitos - Égide

## 1. Visão do Projeto

**Égide** é a plataforma centralizada para gerenciar, distribuir e validar o ciclo de vida de licenças de software, evoluindo de uma ferramenta interna para uma plataforma SaaS (Software as a Service).

## 2. Escopo e Objetivos

* **Objetivo:** Automatizar o processo de vinculação, administração e validação de licenças de software.
* **Escopo Inicial (MVP):** Foco na administração interna (CRUDs de Clientes, Softwares, Licenças) e na API de validação externa.
* **Escopo Futuro:** Multi-tenancy (capacidade de múltiplos inquilinos/empresas usarem o SaaS), painel do cliente, relatórios avançados.

## 3. Requisitos Funcionais (RFs)

| Código | Descrição | Prioridade |
| :--- | :--- | :--- |
| RF001 | O sistema deve permitir o cadastro, edição e consulta de Clientes. | Alta |
| RF002 | O sistema deve permitir o cadastro, edição e consulta de Softwares. | Alta |
| RF003 | O sistema deve permitir a vinculação de uma licença a um Cliente e um Software. | Alta |
| RF004 | O sistema deve suportar 4 tipos de licença: Vitalícia, Por Tempo, Por Usuários e Por Instalações. | Alta |
| RF005 | O sistema deve expor uma API de Administração (interna) para gerenciar Clientes, Softwares e Licenças. | Alta |
| RF006 | O sistema deve expor uma API de Validação (externa) para os Softwares consultarem a validade de suas licenças. | Alta |
| RF007 | O sistema deve receber o cadastro de usuários via API exclusiva (sincronização interna). | Média |

## 4. Requisitos Não Funcionais (RNFs)

| Código | Descrição | Prioridade |
| :--- | :--- | :--- |
| RNF001 | A API de Validação (RF006) deve ter um tempo de resposta médio inferior a 200ms. | Alta |
| RNF002 | A API de Administração (RF005) deve ser protegida por OAuth 2.0. | Alta |
| RNF003 | A API de Validação (RF006) deve ser protegida por API Key (leve e rápida). | Alta |
| RNF004 | A arquitetura deve seguir os princípios da Clean Architecture. | Alta |
| RNF005 | O sistema deve usar .NET 8, Dapper para acesso a dados e PostgreSQL como banco. | Alta |
| RNF006 | O código deve aderir aos princípios SOLID. | Alta |