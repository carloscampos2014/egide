# (ROADMAP.md)
# Roadmap Técnico - Égide

## 1. Objetivo

Este documento define o plano de entregas macro (o "quando") do projeto, alinhado ao Backlog do Produto e à Arquitetura definida. O objetivo é entregar valor de forma incremental, focando no fluxo mínimo viável (MVP) de validação de licenças.

## 2. Fases do Projeto (Sprints)

O roadmap inicial está dividido em 4 Sprints, cobrindo o MVP.

| Sprint | Foco Principal (Entregas) | Histórias de Usuário Chave | Status |
| :--- | :--- | :--- | :--- |
| **Sprint 1** | **Fundação e CRUDs Base** <br> Estrutura da solução (Clean Architecture) e APIs para gerir as entidades centrais. | US01, US02 | Planejado |
| **Sprint 2** | **Vinculação e API de Validação (Simples)** <br> Implementar a lógica de vinculação de licenças simples (Vitalícia, Tempo) e a API de validação para elas. | US03, US05 | Planejado |
| **Sprint 3** | **API de Validação (Contextual)** <br> Evoluir a API de validação para aceitar contexto (usuários/instalações) e validar licenças baseadas em regras. | US04, US06 | Planejado |
| **Sprint 4** | **Sincronização e Administração** <br> Implementar a API de sincronização de usuários (RF007) e as funcionalidades administrativas (ativar/desativar licença). | US07, US08 | Planejado |