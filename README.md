Markdown

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
Passo 2: Configurar a Conexão
O arquivo appsettings.json já está configurado com as credenciais do Oracle da FIAP. Nenhuma alteração é necessária se estiver executando no ambiente da faculdade.

Passo 3: Aplicar as Migrations
Este comando irá criar todas as tabelas no banco de dados. Execute-o a partir da pasta raiz do projeto.

Bash

dotnet ef database update
Passo 4: Executar a API
Bash

dotnet run --project ./
A API estará disponível. A documentação Swagger pode ser acessada em http://localhost:<PORTA>/swagger.

📖 Uso da API (Swagger e cURL)
A documentação completa e interativa de todos os endpoints está disponível via Swagger.

Exemplos de Requisições (cURL)
1. Criar uma nova moto:

Bash

curl -X 'POST' \
  'http://localhost:5188/api/Moto' \
  -H 'Content-Type: application/json' \
  -d '{
  "placa": "XYZ-2025",
  "modelo": "Honda ADV",
  "ano": 2025,
  "status": "DISPONIVEL"
}'
2. Buscar uma moto por ID (com resposta HATEOAS):

Bash

curl -X 'GET' 'http://localhost:5188/api/Moto/1'
Exemplo de Resposta:

JSON

{
  "id": 1,
  "placa": "XYZ-2025",
  "modelo": "Honda ADV",
  "ano": 2025,
  "links": [
    {
      "href": "http://localhost:5188/api/Moto/1",
      "rel": "self",
      "method": "GET"
    },
    {
      "href": "http://localhost:5188/api/Moto/1",
      "rel": "update_moto",
      "method": "PUT"
    },
    {
      "href": "http://localhost:5188/api/Moto/1",
      "rel": "delete_moto",
      "method": "DELETE"
    }
  ]
}



