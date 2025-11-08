# (DEVELOPER_GUIDE.md)
# Manual do Desenvolvedor - Égide

## 1. Ambiente de Desenvolvimento

* [cite_start]**IDE:** Visual Studio 2022+ ou JetBrains Rider[cite: 25].
* [cite_start]**SDK:** .NET 8[cite: 26].
* [cite_start]**Banco de Dados:** PostgreSQL (Recomendado rodar via Docker [cite: 30]).
* [cite_start]**Ferramentas:** Postman ou Swagger (para testes de API)[cite: 29].

## 2. Fluxo de Desenvolvimento (Git)

[cite_start]O projeto segue um fluxo baseado em Pull Requests [cite: 44-50]:

1.  Crie uma *feature branch* a partir da `main` (ex: `feat/RF001-cadastro-cliente`).
2.  Implemente a funcionalidade seguindo os padrões (Clean Architecture, SOLID).
3.  [cite_start]Escreva os testes unitários (xUnit) e de integração[cite: 40, 41]. Garanta que todos passem (`dotnet test`).
4.  Abra um Pull Request (PR) contra a `main`.
5.  O PR deve ser revisado por, no mínimo, 1 outro desenvolvedor.
6.  Após aprovação e sucesso do pipeline de CI, o PR é "squash-merged".

## 3. Padrões de Código

* [cite_start]Siga as convenções do C# (PascalCase, camelCase)[cite: 33].
* [cite_start]Aplique os princípios SOLID [cite: 522-528] [cite_start]e Clean Code [cite: 517-521].
* [cite_start]Use Injeção de Dependência[cite: 35].
* [cite_start]Não duplique lógica[cite: 33].

## 4. Pipeline de CI (GitHub Actions)

[cite_start]O pipeline de CI (`.github/workflows/dotnet-ci.yml`) é ativado em todo PR para a `main` e garante que o código compila e passa nos testes [cite: 191-211].

```yaml
name: Build and Test .NET (Égide)

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v3

    - name: Setup .NET 8
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'

    - name: Restore dependencies
      run: dotnet restore ./src

    - name: Build
      run: dotnet build ./src --no-restore

    - name: Test
      run: dotnet test ./tests --no-build --verbosity normal