# RM555317 - FERNANDO FONTES
# RM556814 - GUILHERME JARDIM


# LINK DO REPOSITÓRIO: https://github.com/xfnd25/MottuLocation.NET

# Mottu Location API

Esta é a API de backend para o sistema de localização de motos da Mottu. Ela permite o gerenciamento de motos, sensores e o registro de movimentações.

## Justificativa da Arquitetura

O projeto foi estruturado seguindo os princípios de uma **Arquitetura em Camadas (Layered Architecture)**, buscando uma clara separação de responsabilidades, o que facilita a manutenção, testabilidade e escalabilidade da aplicação. As camadas são:

* **`Mottu.API` (Camada de Apresentação):** Responsável por expor os endpoints da API. Lida com as requisições HTTP, validação de DTOs e serialização das respostas. Utiliza o padrão de Controllers do ASP.NET Core.
* **`Mottu.Application` (Camada de Aplicação):** Contém a lógica de negócio e orquestra as operações. Utiliza o padrão de **Services** para encapsular as regras e coordenar o acesso aos repositórios. Também é responsável pelo mapeamento entre DTOs e Entidades de domínio.
* **`Mottu.Domain` (Camada de Domínio):** O coração do software. Contém as entidades de negócio (`Motorcycle`, `Deliveryman`, `Rental`), as interfaces dos repositórios e os serviços de domínio. Representa o conhecimento e as regras do negócio.
* **`Mottu.Infrastructure` (Camada de Infraestrutura):** Implementa os detalhes técnicos. Contém a implementação concreta dos repositórios utilizando o **Entity Framework Core** para acesso ao banco de dados, além de outras dependências externas (como serviços de mensageria, logging, etc.).

Essa arquitetura, inspirada em conceitos do DDD (Domain-Driven Design), garante que o domínio do negócio permaneça limpo e independente de tecnologias de infraestrutura.

## Tecnologias Utilizadas

* **.NET 8:** Framework de desenvolvimento backend da Microsoft.
* **ASP.NET Core:** Plataforma para construir aplicações web modernas.
* **Entity Framework Core:** ORM (Object-Relational Mapper) para interagir com o banco de dados.
* **Oracle:** Banco de dados utilizado para persistência dos dados.
* **AutoMapper:** Biblioteca para mapeamento de objetos.
* **Swashbuckle.AspNetCore:** Ferramenta para geração de documentação OpenAPI (Swagger).

## Pré-requisitos

Antes de executar a API, certifique-se de ter o seguinte instalado no seu sistema:

* **.NET 8 SDK:** Você pode baixá-lo em [https://dotnet.microsoft.com/download/dotnet/8.0](https://dotnet.microsoft.com/download/dotnet/8.0).
* **Acesso a um servidor Oracle:** A API requer uma instância do banco de dados Oracle configurada.
* **Postman ou outra ferramenta de teste de API:** Recomendado para interagir com os endpoints da API.

## Documentação da API (Swagger)

A API utiliza o Swagger para gerar automaticamente a documentação dos endpoints. Para acessar a documentação:

1.  Abra o seu navegador.
2.  Navegue até o endpoint do Swagger (geralmente em `http://localhost:5xxx/swagger/index.html`, substituindo `5xxx` pela porta em que a sua API está rodando).

Na página do Swagger, você poderá explorar todos os endpoints disponíveis, os métodos HTTP, os parâmetros, os corpos das requisições e os exemplos de respostas.




## 📡 Endpoints da API - Motolocation

> **URL base**: `http://localhost:8080` (ou IP da sua VM)

---

### 🏍️ MotoController (`/api/moto`)

#### 🔸 POST `/api/moto`  
Cria uma nova moto. O `rfidTag` é gerado automaticamente.

**Exemplo de body JSON:**
```json
{
  "placa": "ABC1234",
  "modelo": "Honda CBR 600RR",
  "ano": 2024,
  "status": "DISPONIVEL",
  "observacoes": "Moto esportiva nova."
}
🔸 GET /api/moto/{id}
Obtém uma moto específica pelo ID.
Exemplo: GET /api/moto/1

🔸 PUT /api/moto/{id}
Atualiza uma moto existente.

Body JSON (para id=1):
{
  "id": 1,
  "placa": "ABC1234",
  "modelo": "Honda CBR 600RR",
  "ano": 2024,
  "rfidTag": "RFID-XYZ789",
  "status": "EM_MANUTENCAO",
  "observacoes": "Problema no freio dianteiro."
}
Obs: id e rfidTag devem coincidir com a moto original.

🔸 DELETE /api/moto/{id}
Remove uma moto pelo ID.
Exemplo: DELETE /api/moto/1

🔸 GET /api/moto
Lista motos com suporte a paginação, ordenação e filtro.

Exemplos:

GET /api/moto

GET /api/moto?page=1&size=5&sortBy=modelo

GET /api/moto?placaFiltro=ABC1234

📍 SensorController (/api/sensor)
🔸 POST /api/sensor
Cria um novo sensor.

Body JSON:
{
  "codigo": "SENSOR001",
  "posicaoX": 10,
  "posicaoY": 20,
  "descricao": "Sensor de entrada do estacionamento."
}
🔸 GET /api/sensor/{id}
Busca um sensor pelo ID.
Exemplo: GET /api/sensor/1

🔸 PUT /api/sensor/{id}
Atualiza os dados de um sensor.

Body JSON (para id=1):
{
  "id": 1,
  "codigo": "SENSOR001",
  "posicaoX": 15,
  "posicaoY": 25,
  "descricao": "Sensor de entrada do estacionamento (atualizado)."
}
Obs: id e codigo devem coincidir com o sensor original.

🔸 DELETE /api/sensor/{id}
Remove um sensor pelo ID.
Exemplo: DELETE /api/sensor/1

🔸 GET /api/sensor
Lista sensores com suporte a paginação, ordenação e filtro.

Exemplos:

GET /api/sensor

GET /api/sensor?page=0&size=5&sortBy=codigo

GET /api/sensor?codigoFiltro=SENSOR001

🕘 MovimentacaoController (/api/movimentacao)
🔸 POST /api/movimentacao
Registra uma movimentação (simulando a leitura de RFID por sensor).

Body JSON:
{
  "rfid": "RFID-XYZ789",
  "sensorCodigo": "SENSOR001"
}
🔸 GET /api/movimentacao/moto/{motoId}
Lista todas as movimentações de uma moto específica.

Exemplos:

GET /api/movimentacao/moto/1

GET /api/movimentacao/moto/1?page=0&size=5&sortBy=dataHora

...
