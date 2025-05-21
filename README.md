# RM555317 - FERNANDO FONTES
# RM556814 - GUILHERME JARDIM

Mottu Location API
Bem-vindo à Mottu Location API! Este é o backend do sistema de localização de motos da Mottu, desenvolvido em .NET. Ele oferece funcionalidades para gerenciar motos, sensores e registrar suas movimentações em tempo real.

🚀 Tecnologias Utilizadas
Este projeto foi construído com as seguintes tecnologias e ferramentas:

.NET 8: O mais recente framework de desenvolvimento da Microsoft para aplicações robustas e performáticas.
ASP.NET Core: Plataforma para a criação de APIs web modernas e escaláveis.
Entity Framework Core: Um poderoso ORM (Object-Relational Mapper) que simplifica a interação com o banco de dados.
Oracle Database: O sistema de gerenciamento de banco de dados relacional utilizado para persistir as informações.
AutoMapper: Uma biblioteca para mapeamento de objetos que reduz o código repetitivo entre camadas.
Swashbuckle.AspNetCore: Gerador automático de documentação OpenAPI (Swagger UI) para facilitar a exploração e teste da API.

📋 Pré-requisitos
Para executar este projeto em sua máquina local, você precisará ter o seguinte instalado:

SDK do .NET 8: Baixe e instale a versão mais recente em dotnet.microsoft.com/download/dotnet/8.0.
Acesso a uma Instância do Oracle Database: A API precisa se conectar a um banco de dados Oracle. Certifique-se de ter as credenciais e o acesso necessários.
Postman (ou similar): Ferramenta essencial para testar os endpoints da API.

Aqui está um README de alta qualidade, completo e bem estruturado para o seu projeto .NET, com foco em clareza e facilidade de uso:

Mottu Location API
Bem-vindo à Mottu Location API! Este é o backend do sistema de localização de motos da Mottu, desenvolvido em .NET. Ele oferece funcionalidades para gerenciar motos, sensores e registrar suas movimentações em tempo real.

🚀 Tecnologias Utilizadas
Este projeto foi construído com as seguintes tecnologias e ferramentas:

.NET 8: O mais recente framework de desenvolvimento da Microsoft para aplicações robustas e performáticas.
ASP.NET Core: Plataforma para a criação de APIs web modernas e escaláveis.
Entity Framework Core: Um poderoso ORM (Object-Relational Mapper) que simplifica a interação com o banco de dados.
Oracle Database: O sistema de gerenciamento de banco de dados relacional utilizado para persistir as informações.
AutoMapper: Uma biblioteca para mapeamento de objetos que reduz o código repetitivo entre camadas.
Swashbuckle.AspNetCore: Gerador automático de documentação OpenAPI (Swagger UI) para facilitar a exploração e teste da API.

📋 Pré-requisitos
Para executar este projeto em sua máquina local, você precisará ter o seguinte instalado:

SDK do .NET 8: Baixe e instale a versão mais recente em dotnet.microsoft.com/download/dotnet/8.0.
Acesso a uma Instância do Oracle Database: A API precisa se conectar a um banco de dados Oracle. Certifique-se de ter as credenciais e o acesso necessários.
Postman (ou similar): Ferramenta essencial para testar os endpoints da API.
🛠️ Configuração do Projeto
Siga estes passos para configurar e executar a API em sua máquina:

Clone o Repositório:
Se o seu projeto estiver em um repositório Git, clone-o:

git clone <URL_DO_SEU_REPOSITORIO>
cd MottuLocation
Caso contrário, apenas navegue até a pasta raiz do projeto.

Configure a String de Conexão do Banco de Dados:

Abra o arquivo appsettings.json na raiz do projeto.

Localize a seção "ConnectionStrings" e atualize o valor de "OracleConnection" com as suas credenciais do Oracle Database:
{
  "ConnectionStrings": {
    "OracleConnection": "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=SEU_HOST)(PORT=SEU_PORTA)))(CONNECT_DATA=(SERVICE_NAME=SEU_SERVICO)));User ID=SEU_USUARIO;Password=SUA_SENHA;"
  },
}
Não se esqueça de substituir SEU_HOST, SEU_PORTA, SEU_SERVICO, SEU_USUARIO e SUA_SENHA pelos valores corretos do seu ambiente.

Aqui está um README de alta qualidade, completo e bem estruturado para o seu projeto .NET, com foco em clareza e facilidade de uso:

Mottu Location API
Bem-vindo à Mottu Location API! Este é o backend do sistema de localização de motos da Mottu, desenvolvido em .NET. Ele oferece funcionalidades para gerenciar motos, sensores e registrar suas movimentações em tempo real.

🚀 Tecnologias Utilizadas
Este projeto foi construído com as seguintes tecnologias e ferramentas:

.NET 8: O mais recente framework de desenvolvimento da Microsoft para aplicações robustas e performáticas.
ASP.NET Core: Plataforma para a criação de APIs web modernas e escaláveis.
Entity Framework Core: Um poderoso ORM (Object-Relational Mapper) que simplifica a interação com o banco de dados.
Oracle Database: O sistema de gerenciamento de banco de dados relacional utilizado para persistir as informações.
AutoMapper: Uma biblioteca para mapeamento de objetos que reduz o código repetitivo entre camadas.
Swashbuckle.AspNetCore: Gerador automático de documentação OpenAPI (Swagger UI) para facilitar a exploração e teste da API.
📋 Pré-requisitos
Para executar este projeto em sua máquina local, você precisará ter o seguinte instalado:

SDK do .NET 8: Baixe e instale a versão mais recente em dotnet.microsoft.com/download/dotnet/8.0.
Acesso a uma Instância do Oracle Database: A API precisa se conectar a um banco de dados Oracle. Certifique-se de ter as credenciais e o acesso necessários.
Postman (ou similar): Ferramenta essencial para testar os endpoints da API.
🛠️ Configuração do Projeto
Siga estes passos para configurar e executar a API em sua máquina:

Clone o Repositório:
Se o seu projeto estiver em um repositório Git, clone-o:

git clone <URL_DO_SEU_REPOSITORIO>
cd MottuLocation
Caso contrário, apenas navegue até a pasta raiz do projeto.

Configure a String de Conexão do Banco de Dados:

Abra o arquivo appsettings.json na raiz do projeto.

Localize a seção "ConnectionStrings" e atualize o valor de "OracleConnection" com as suas credenciais do Oracle Database:

JSON

{
  "ConnectionStrings": {
    "OracleConnection": "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=SEU_HOST)(PORT=SEU_PORTA)))(CONNECT_DATA=(SERVICE_NAME=SEU_SERVICO)));User ID=SEU_USUARIO;Password=SUA_SENHA;"
  },
  // ... outras configurações ...
}
Não se esqueça de substituir SEU_HOST, SEU_PORTA, SEU_SERVICO, SEU_USUARIO e SUA_SENHA pelos valores corretos do seu ambiente.

▶️ Como Rodar a API
Navegue até a pasta raiz do projeto no seu terminal (cmd, PowerShell, ou terminal do VS Code):

cd MottuLocation
Restaure as dependências NuGet:

dotnet restore
Execute as Migrações do Banco de Dados (se for a primeira vez ou se houver novas migrações):
Certifique-se de que o banco de dados e as tabelas estejam criados com as migrações do Entity Framework Core:

dotnet ef database update
(Se você não tiver as ferramentas do EF Core instaladas globalmente, execute dotnet tool install --global dotnet-ef primeiro.)

Inicie a Aplicação:
dotnet run
A API será compilada e iniciada. No terminal, você verá as URLs em que a aplicação está rodando (geralmente http://localhost:5xxx e https://localhost:7xxx). Anote a porta HTTP, pois ela será usada para os testes.

🧪 Testando a API com Postman
Aqui estão as requisições essenciais que você pode fazer no Postman para testar os principais endpoints da API:

Observação: Lembre-se de substituir http://localhost:SEU_PORTA pela URL base da sua API.

MotoController (/api/moto)
Criar uma Moto (POST)

URL: http://localhost:SEU_PORTA/api/moto
Método: POST
Corpo da Requisição (JSON - selecione "raw" e "JSON"):
{
    "placa": "XYZ-1234",
    "modelo": "Honda CB 500F",
    "ano": 2023,
    "status": "Disponível",
    "observacoes": "Moto nova para locação"
}
Resposta: 201 Created com os dados da moto criada (incluindo Id e RfidTag).
Obter Moto por ID (GET)

URL: http://localhost:SEU_PORTA/api/moto/{id} (Ex: http://localhost:SEU_PORTA/api/moto/1)
Método: GET
Resposta: 200 OK com os dados da moto ou 404 Not Found.
Atualizar Moto (PUT)

URL: http://localhost:SEU_PORTA/api/moto/{id} (substitua {id} pelo ID da moto a ser atualizada)
Método: PUT
Corpo da Requisição (JSON - selecione "raw" e "JSON"):
{
    "id": 1, // **Obrigatório: Use o ID da moto que você está atualizando**
    "placa": "XYZ-1234",
    "modelo": "Honda CB 500F Atualizada",
    "ano": 2023,
    "status": "Em Manutenção",
    "observacoes": "Revisão periódica"
}
Resposta: 200 OK com os dados da moto atualizada ou 404 Not Found.
Excluir Moto (DELETE)

URL: http://localhost:SEU_PORTA/api/moto/{id} (substitua {id} pelo ID da moto a ser excluída)
Método: DELETE
Resposta: 204 No Content ou 404 Not Found.
Listar Motos (GET)

URL: http://localhost:SEU_PORTA/api/moto
Método: GET
Parâmetros de Query (opcionais):
page: 0 (padrão)
size: 10 (padrão)
sortBy: placa (padrão), modelo, ano
placaFiltro: Filtra por parte da placa (Ex: ABC)
Exemplo: http://localhost:SEU_PORTA/api/moto?page=0&size=5&sortBy=ano&placaFiltro=XYZ
Resposta: 200 OK com uma lista paginada de motos.
SensorController (/api/sensor)
Criar Sensor (POST)

URL: http://localhost:SEU_PORTA/api/sensor
Método: POST
Corpo da Requisição (JSON):
{
    "codigo": "AREA_A_001",
    "posicaoX": 100,
    "posicaoY": 250,
    "descricao": "Sensor de entrada da garagem principal"
}
Resposta: 201 Created com os dados do sensor criado.
Obter Sensor por ID (GET)

URL: http://localhost:SEU_PORTA/api/sensor/{id} (substitua {id})
Método: GET
Resposta: 200 OK ou 404 Not Found.
Atualizar Sensor (PUT)

URL: http://localhost:SEU_PORTA/api/sensor/{id} (substitua {id})
Método: PUT
Corpo da Requisição (JSON):
{
    "id": 1, // **Obrigatório: Use o ID do sensor que você está atualizando**
    "codigo": "AREA_A_001",
    "posicaoX": 105,
    "posicaoY": 255,
    "descricao": "Sensor de entrada da garagem principal (atualizado)"
}
Resposta: 200 OK ou 404 Not Found.
Excluir Sensor (DELETE)

URL: http://localhost:SEU_PORTA/api/sensor/{id} (substitua {id})
Método: DELETE
Resposta: 204 No Content ou 404 Not Found.
Listar Sensores (GET)

URL: http://localhost:SEU_PORTA/api/sensor
Método: GET
Parâmetros de Query (opcionais):
page: 0 (padrão)
size: 10 (padrão)
sortBy: codigo (padrão), descricao
codigoFiltro: Filtra por parte do código (Ex: AREA)
Exemplo: http://localhost:SEU_PORTA/api/sensor?size=5&sortBy=descricao&codigoFiltro=A_0
Resposta: 200 OK com uma lista paginada de sensores.
MovimentacaoController (/api/movimentacao)
Registrar Movimentação (POST)

Pré-requisito: Tenha uma Moto e um Sensor já criados no banco de dados.
URL: http://localhost:SEU_PORTA/api/movimentacao
Método: POST
Corpo da Requisição (JSON):
{
    "rfid": "RFID_DA_MOTO_EXISTENTE",    // Use o RfidTag de uma moto já criada
    "sensorCodigo": "CODIGO_DO_SENSOR_EXISTENTE" // Use o Código de um sensor já criado
}
Resposta: 201 Created com os detalhes da movimentação ou 404 Not Found se a moto ou o sensor não existirem.
Listar Movimentações por Moto (GET)

URL: http://localhost:SEU_PORTA/api/movimentacao/moto/{motoId} (substitua {motoId} pelo ID da moto)
Método: GET
Parâmetros de Query (opcionais):
page: 0 (padrão)
size: 10 (padrão)
sortBy: dataHora (padrão), motoId, sensorId
Exemplo: http://localhost:SEU_PORTA/api/movimentacao/moto/123?page=0&size=10&sortBy=dataHora
Resposta: 200 OK com uma lista paginada das movimentações da moto ou 404 Not Found.
