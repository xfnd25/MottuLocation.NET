# API - Mottu Location .NET

[![Status do Projeto](https://img.shields.io/badge/status-concluído-green)](https://github.com/xfnd25/MottuLocation.NET)

API RESTful desenvolvida em .NET 8 para o sistema de gerenciamento e localização de motos, como projeto para a disciplina "Advanced Business Development with .NET".

---

## Integrantes

* **RM555317** - FERNANDO FONTES
* **RM556814** - GUILHERME JARDIM

---

## 🏛️ Justificativa da Arquitetura

O projeto foi estruturado seguindo os princípios de uma **Arquitetura em Camadas (Layered Architecture)** para garantir uma clara separação de responsabilidades, facilitando a manutenção e escalabilidade.

* **`MottuLocation.API` (Apresentação):** Expõe os endpoints RESTful, lida com requisições HTTP e validação de DTOs.
* **`MottuLocation.Application` (Aplicação):** Orquestra as operações e contém a lógica de negócio principal, utilizando o padrão de Services.
* **`MottuLocation.Domain` (Domínio):** O núcleo do software, contendo as entidades de negócio (`Moto`, `Sensor`, `Movimentacao`) e as interfaces de repositórios.
* **`MottuLocation.Infrastructure` (Infraestrutura):** Implementa os detalhes técnicos, como o acesso ao banco de dados Oracle com Entity Framework Core.

---

## 🛠️ Tecnologias e Boas Práticas

* **.NET 8** e ASP.NET Core
* **Entity Framework Core 8** com **Oracle**
* **AutoMapper** para mapeamento de objetos
* **Swagger/OpenAPI** para documentação interativa
* **Versionamento de API**
* **Segurança de API com API Key**
* **Health Checks**
* **Boas Práticas REST:**
    * Uso correto de verbos HTTP e Status Codes (`201 Created`, `204 No Content`, etc.).
    * Suporte a **Paginação**, ordenação e filtro.
    * Implementação de **HATEOAS** (Hypermedia as the Engine of Application State) para descoberta de ações.

---

## 🚀 Instruções de Execução

### Pré-requisitos
* **[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)**
* Acesso a um servidor **Oracle** com as credenciais fornecidas.

### Passo 1: Clonar o Repositório
```bash
git clone [https://github.com/xfnd25/MottuLocation.NET.git](https://github.com/xfnd25/MottuLocation.NET.git)
cd MottuLocation.NET
```
### Passo 2: Configurar a Conexão
O arquivo `appsettings.json` já está configurado com as credenciais do Oracle da FIAP. Nenhuma alteração é necessária se estiver executando no ambiente da faculdade.

### Passo 3: Aplicar as Migrations
Este comando irá criar todas as tabelas no banco de dados. Execute-o a partir da pasta raiz do projeto.

```bash
dotnet ef database update
```

### Passo 4: Executar a API
```bash
dotnet run --project ./
```
A API estará disponível. A documentação Swagger pode ser acessada em http://localhost:<PORTA>/swagger.

---

## 🧪 Executando os Testes
Para rodar a suíte de testes de unidade e integração, execute o seguinte comando na raiz do projeto:
```bash
dotnet test
```

---

## ✨ Novas Funcionalidades

### Versionamento de API
A API agora suporta versionamento. A versão atual é a `v1`. Para acessar os endpoints, utilize o prefixo `/api/v1/` na URL.

**Exemplo:**
```bash
curl -X 'GET' 'http://localhost:5188/api/v1/moto/1'
```

### Segurança com API Key
Todos os endpoints agora são protegidos por uma chave de API. Para acessar a API, você deve incluir a chave no header da requisição, com o nome `X-API-KEY`.

**Exemplo:**
```bash
curl -X 'GET' 'http://localhost:5188/api/v1/moto/1' \
  -H 'X-API-KEY: MinhaChaveDeApiSuperSecreta'
```
A chave de API padrão está configurada no arquivo `appsettings.json`.

### Health Checks
A API agora expõe um endpoint de Health Check em `/health`. Este endpoint pode ser utilizado para monitorar a saúde da aplicação e de suas dependências (como o banco de dados).

**Exemplo:**
```bash
curl -X 'GET' 'http://localhost:5188/health'
```

---

📖 Uso da API (Swagger e cURL)
A documentação completa e interativa de todos os endpoints está disponível via Swagger.

Exemplos de Requisições (cURL)
1. Criar uma nova moto:

```bash
curl -X 'POST' \
  'http://localhost:5188/api/v1/moto' \
  -H 'Content-Type: application/json' \
  -H 'X-API-KEY: MinhaChaveDeApiSuperSecreta' \
  -d '{
  "placa": "XYZ-2025",
  "modelo": "Honda ADV",
  "ano": 2025,
  "status": "DISPONIVEL"
}'
```
2. Buscar uma moto por ID (com resposta HATEOAS):

```bash
curl -X 'GET' 'http://localhost:5188/api/v1/moto/1' \
  -H 'X-API-KEY: MinhaChaveDeApiSuperSecreta'
```
Exemplo de Resposta:

```json
{
  "id": 1,
  "placa": "XYZ-2025",
  "modelo": "Honda ADV",
  "ano": 2025,
  "links": [
    {
      "href": "http://localhost:5188/api/v1/moto/1",
      "rel": "self",
      "method": "GET"
    },
    {
      "href": "http://localhost:5188/api/v1/moto/1",
      "rel": "update_moto",
      "method": "PUT"
    },
    {
      "href": "http://localhost:5188/api/v1/moto/1",
      "rel": "delete_moto",
      "method": "DELETE"
    }
  ]
}
```
