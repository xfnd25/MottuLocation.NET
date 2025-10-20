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

* **Apresentação (`Controllers`):** Expõe os endpoints RESTful, lida com requisições HTTP e validação de DTOs.
* **Aplicação (`Services`):** Orquestra as operações e contém a lógica de negócio principal.
* **Domínio (`Entities`, `Repositories`):** O núcleo do software, contendo as entidades de negócio (`Moto`, `Sensor`, `Movimentacao`) e as interfaces de repositórios.
* **Infraestrutura (`Data`):** Implementa os detalhes técnicos, como o acesso ao banco de dados Oracle com Entity Framework Core.

---

## 🛠️ Tecnologias e Boas Práticas

* **.NET 8** e ASP.NET Core
* **Entity Framework Core 8** com **Oracle**
* **ML.NET** para funcionalidades de Machine Learning
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
* [**.NET 8 SDK**](https://dotnet.microsoft.com/download/dotnet/8.0)
* Acesso a um servidor **Oracle** com as credenciais configuradas.

### Passo 1: Clonar o Repositório
```bash
git clone [https://github.com/xfnd25/MottuLocation.NET.git](https://github.com/xfnd25/MottuLocation.NET.git)
cd MottuLocation.NET
```

### Passo 2: Configurar a Conexão
O arquivo `appsettings.json` deve ser configurado com a string de conexão do Oracle.

### Passo 3: Aplicar as Migrations
Este comando irá criar todas as tabelas no banco de dados. Execute-o a partir da pasta raiz do projeto.
```bash
dotnet ef database update
```

### Passo 4: Executar a API
```bash
dotnet run
```
A API estará disponível. A documentação Swagger pode ser acessada em http://localhost:&lt;PORTA&gt;/swagger.

---

## 🧪 Executando os Testes
Para rodar a suíte de testes de unidade e integração, execute o seguinte comando na raiz do projeto:
```bash
dotnet test
```

---

## ✨ Funcionalidades Avançadas

### Versionamento de API
A API agora suporta versionamento. A versão atual é a `v1`. Para acessar os endpoints, utilize o prefixo `/api/v1/` na URL.

### Segurança com API Key
Todos os endpoints (exceto `/health` e `/swagger`) são protegidos por uma chave de API. Para acessar a API, você deve incluir a chave no header da requisição, com o nome `X-API-KEY`. A chave padrão está configurada no arquivo `appsettings.json`.

### Health Checks
A API expõe um endpoint público de Health Check em `/health` para monitorar a saúde da aplicação e de suas dependências (como o banco de dados).

### Previsão de Manutenção com ML.NET
A API agora inclui um endpoint que utiliza **Machine Learning** (com a biblioteca ML.NET) para prever a probabilidade de uma moto necessitar de manutenção. O modelo foi treinado com base no ano da moto e seu histórico de movimentações.

---

## 📖 Referência da API (Endpoints)

A documentação interativa completa está disponível via **Swagger**. Com a API rodando, acesse a interface para testar os endpoints de forma prática.

**Como usar a API Key no Swagger:**
1. Acesse a página do Swagger.
2. Clique no botão **"Authorize"** no canto superior direito.
3. Insira a sua API Key no campo "Value" e clique em "Authorize". A chave será enviada automaticamente em todas as requisições.

### Moto

#### `POST /api/v1/Moto`
Cria uma nova moto no sistema.
* **Request Body:**
  ```json
  {
    "placa": "BRA2E19",
    "modelo": "Honda PCX",
    "ano": 2024
  }
  ```
* **Success Response (201 Created):**
  ```json
  {
    "id": 1,
    "placa": "BRA2E19",
    "modelo": "Honda PCX",
    "ano": 2024,
    "rfidTag": "gerado-automaticamente",
    "links": [
      { "href": "http://localhost:5188/api/v1/Moto/1", "rel": "self", "method": "GET" },
      { "href": "http://localhost:5188/api/v1/Moto/1", "rel": "update_moto", "method": "PUT" },
      { "href": "http://localhost:5188/api/v1/Moto/1", "rel": "delete_moto", "method": "DELETE" }
    ]
  }
  ```

#### `GET /api/v1/Moto`
Lista todas as motos com suporte a paginação.
* **Query Parameters:**
  * `pageNumber` (int): Número da página (padrão: 1).
  * `pageSize` (int): Itens por página (padrão: 10).
* **Success Response (200 OK):** Retorna uma lista de motos.

#### `GET /api/v1/Moto/{id}`
Busca uma moto específica pelo seu ID.

#### `PUT /api/v1/Moto/{id}`
Atualiza os dados de uma moto existente.
* **Request Body:**
  ```json
  {
    "placa": "BRA2E19",
    "modelo": "Honda PCX DLX",
    "ano": 2024,
    "status": "EM_MANUTENCAO"
  }
  ```
* **Success Response (200 OK):** Retorna o objeto da moto atualizada.

#### `DELETE /api/v1/Moto/{id}`
Remove uma moto do sistema.
* **Success Response (204 No Content):** Nenhuma resposta no corpo.

### Sensor

#### `POST /api/v1/Sensor`
Cria um novo sensor no sistema.
* **Request Body:**
  ```json
  {
    "codigo": "PATIO-A-01",
    "posicaoX": 10,
    "posicaoY": 25,
    "descricao": "Sensor do portão de entrada"
  }
  ```
* **Success Response (201 Created):** Retorna o objeto do sensor criado com seus links HATEOAS.

#### `GET /api/v1/Sensor`
Lista todos os sensores com suporte a paginação.

#### `GET /api/v1/Sensor/{id}`
Busca um sensor específico pelo seu ID.

#### `PUT /api/v1/Sensor/{id}`
Atualiza os dados de um sensor existente.

#### `DELETE /api/v1/Sensor/{id}`
Remove um sensor do sistema.

### Movimentacao

#### `POST /api/v1/Movimentacao`
Registra uma nova movimentação de moto, associando um RFID a um código de sensor.
* **Request Body:**
  ```json
  {
    "rfid": "rfid-da-moto-aqui",
    "sensorCodigo": "PATIO-A-01"
  }
  ```
* **Success Response (201 Created):** Retorna o objeto da movimentação registrada.

#### `GET /api/v1/Movimentacao/moto/{motoId}`
Lista todas as movimentações de uma moto específica.

### Prediction (ML.NET)

#### `POST /api/v1/Prediction`
Prevê a necessidade de manutenção de uma moto.
* **Request Body:**
  ```json
  {
    "ano": 2022,
    "totalMovimentacoes": 550
  }
  ```
* **Success Response (200 OK):**
  ```json
  {
    "predictedLabel": true,
    "score": 0.952
  }
  ```
  * `predictedLabel`: `true` se a manutenção é recomendada.
  * `score`: A confiança do modelo na previsão (probabilidade).