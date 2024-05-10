# Desafio Prático - Processo Seletivo AutoGlass
- Objetivo: Criar uma API de Gestão de Produtos

---

# Tabelas

## Produtos
- Id
- Descricao
- Situacao
- DataFabricacao
- DataValidade
- FornecedorId

## Fornecedores
- Id
- Descricao
- Cnpj
- Situacao

---

# Recursos da API

## Produto
- InserirProduto
- AtualizarProduto
- RemoverProduto
- ListarProdutos
- ListarProdutosComFiltroEPaginacao
- RecuperarProdutoPorId

## Fornecedor
- InserirFornecedor
- AtualizarFornecedor
- RemoverFornecedor
- ListarFornecedores
- RecuperarFornecedorPorId
- RecuperarFornecedorPorCnpj

---

# Definições da API

## Tecnologia
- .NET Core 5
## Arquitetura
- Camadas e DDD
## ORM
- EF Core
## DTO
- AutoMapper
## Testes
- Pirâmide de Testes (TDD)

## Extensões utilizadas
- Fine Code Coverage (Validação de cobertura de teste das classes)
- SonarLint (Melhoria de código em tempo real)
- Stryker Dotnet (Teste de Mutação) - PENDENTE
