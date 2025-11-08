# (RISK_PLAN.md)
# Plano de Riscos - Égide

## 1. Objetivo

Este documento identifica os riscos técnicos e operacionais do projeto Égide, definindo a probabilidade, o impacto e as estratégias de mitigação para garantir o sucesso das entregas.

## 2. Matriz de Riscos

| Risco | Probabilidade | Impacto | Estratégia de Mitigação |
| :--- | :--- | :--- | :--- |
| **Degradação de performance na API de Validação (RNF001)** <br> (A API externa fica lenta sob alta carga, impactando o software do cliente). | Média | **Alto** | 1. Implementar uma camada de *caching* (ex: Redis) para licenças ativas. <br> 2. Otimizar rigorosamente as consultas Dapper. <br> 3. Realizar testes de carga (stress tests) antes do deploy da Sprint 2. |
| **Atraso na definição da API de Usuários (RF007)** <br> (A API interna de sincronização de usuários não fica pronta a tempo, bloqueando o US07). | Alta | Média | 1. Definir o contrato (schema) da API com a outra equipa imediatamente. <br> 2. Desenvolver a funcionalidade de licença (US06) usando um *mock* (endpoint falso) para não haver bloqueio. |
| **Complexidade no mapeamento do Domínio com Dapper** <br> (Dificuldade em mapear o modelo polimórfico de `Licencas` para o PostgreSQL sem um ORM completo como EF Core). | Média | Média | 1. Manter o esquema da DB simples (ex: tabela única `Licencas` com uma coluna "Tipo"). <br> 2. Focar os testes de integração (Sprint 2) especificamente neste mapeamento. |
| **Dificuldade na migração para Multi-Tenancy (SaaS)** <br> (Decisões de arquitetura da versão interna dificultam a separação de dados por "inquilino" na versão SaaS). | Baixa | **Alto** | 1. [cite_start]Seguir rigorosamente a Clean Architecture [cite: 395-485] para isolar a lógica de negócio. <br> 2. Adicionar o campo `TenantId` (ID do Inquilino) às tabelas principais (`Clientes`, `Softwares`, `Licencas`) desde o início, mesmo que não seja usado na versão interna. |