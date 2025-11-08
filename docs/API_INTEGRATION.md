# (API_INTEGRATION.md)
# Manual de Integração - Égide APIs

[cite_start]Este documento detalha as APIs expostas pela plataforma Égide[cite: 431, 432].

## 1. API de Administração (Interna)

* **Objetivo:** Gerenciamento (CRUD) das entidades do sistema.
* **Autenticação:** OAuth 2.0 (Client Credentials).
* **Base URL:** `https://api.egide.interno/api/v1`

### Endpoints (Exemplos)

#### `POST /clientes`
Cria um novo cliente.
* **Body:**
    ```json
    {
      "nome": "Cliente Exemplo SA",
      "documento": "XX.XXX.XXX/0001-XX"
    }
    ```

#### `POST /licencas/vincular/tempo`
Vincula uma licença baseada em tempo.
* **Body:**
    ```json
    {
      "clienteId": "guid-do-cliente",
      "softwareId": "guid-do-software",
      "dataExpiracao": "2026-01-01T00:00:00Z"
    }
    ```

## 2. API de Validação (Externa)

* **Objetivo:** Consumida pelos softwares dos clientes para validar suas licenças. Deve ser rápida e leve.
* **Autenticação:** API Key (enviada no Header `X-Egide-Token`).
* **Base URL:** `https://check.egide.io`

### Endpoint Principal

#### `POST /check`
Verifica a situação da licença atual.
* **Body:**
    ```json
    {
      "softwareToken": "GUID-DO-SOFTWARE",
      "clienteToken": "GUID-DO-CLIENTE",
      "context": {
        "contagemUsuariosAtuais": 15,
        "contagemInstalacoesAtuais": 30
      }
    }
    ```
* **Resposta de Sucesso (200 OK):**
    ```json
    {
      "valida": true,
      "tipoLicenca": "PorUsuario",
      "mensagem": "Licença válida."
    }
    ```
* **Resposta de Falha (200 OK ou 403 Forbidden):**
    ```json
    {
      "valida": false,
      "tipoLicenca": "PorUsuario",
      "mensagem": "Limite de usuários (10) atingido."
    }
    ```