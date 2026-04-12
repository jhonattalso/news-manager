# News Manager
> **Challenge - Sprint 3 | FIAP**
>
> Advanced Business Development With .NET

O **News-Manager** é uma plataforma central de gerenciamento de notícias e comunicados. O projeto permite que administradores cadastrem, editem e publiquem conteúdos relevantes, garantindo que investidores e empreendedores estejam sempre atualizados.

A aplicação foi desenvolvida em **ASP.NET Core MVC** e utiliza o **Oracle Database** para persistência robusta de dados

### Integrantes do Grupo
* **Jhonatta Lima Sandes de Oliveira** - RM: 560277
* **Rangel Bernadi Jordão** - RM: 560547
* **Lucas José Lima** - RM: 561160

**Turma:** 2TDSPA


---

## Decisões Arquiteturais

O projeto segue a arquitetura MVC (Model-View-Controller), com foco em separação de responsabilidades e alta manutenibilidade.

* **Framework:** .NET 9 / ASP.NET Core MVC.
* **ORM:** Entity Framework Core (EF Core) para manipulação de dados.
* **Banco de Dados:** Oracle Database.
* **Design Patterns:**
    * **Dependency Injection:** Injeção de dependências nativa para INewsService e INewsRepository.
    * **Service Layer:** Camada de serviço (NewsService.cs) para isolar regras de negócio.
    * **Repository Pattern:** Desacoplamento do acesso a dados via NewsRepository.cs
* **Front-end:** Razor Views com Bootstrap para estilização responsiva e layout consistente.

---

## Monitoramento e Observabilidade (Novo)

Implementamos ferramentas de diagnóstico para garantir que a aplicação opere de forma saudável e que erros possam ser rastreados rapidamente.

* **Health Checks**: Endpoint configurado em /health para monitorar a saúde da aplicação e a conectividade com o banco de dados Oracle.
* **Logging Estruturado**: Utilização do Serilog para geração de logs detalhados, com saída simultânea para o Console e arquivos locais de texto.
* **Tracing e Métricas**: Integração com OpenTelemetry para rastrear o tempo de execução de requisições e monitorar o desempenho entre as camadas de serviço e repositório. 

---

## Rotas e Navegação (Endpoints)

A aplicação utiliza **Attribute Routing** para definir URLs amigáveis e descritivas, facilitando a navegação e a manutenção do sistema.

| Funcionalidade | Método HTTP | Rota / Endpoint | Descrição | Acesso |
| :--- | :---: | :--- | :--- | :---: |
| **Home / Listagem** | `GET` | `/` ou `/lista` | Página principal que lista todas as notícias com suporte a busca. | Público |
| **Health Check** | `GET` | `/health` | Endpoint técnico que verifica a saúde da aplicação e do banco Oracle. | Público |
| **Visualizar Notícia** | `GET` | `/detalhes/{id}` | Exibe o conteúdo completo, autor e data de publicação de uma notícia. | Público |
| **Criar Notícia (Tela)** | `GET` | `/cadastrar` | Exibe o formulário para cadastro de uma nova notícia. | Público |
| **Criar Notícia (Ação)** | `POST` | `/cadastrar` | Processa os dados do formulário e salva a nova notícia no banco. | Público |
| **Editar Notícia (Tela)** | `GET` | `/editar/{id}` | Exibe o formulário de edição preenchido com os dados atuais da notícia. | Público |
| **Editar Notícia (Ação)** | `POST` | `/editar/{id}` | Processa as alterações realizadas na notícia e atualiza o registro. | Público |
| **Excluir Notícia (Tela)** | `GET` | `/excluir/{id}` | Exibe uma página de confirmação antes de remover a notícia. | Público |
| **Excluir Notícia (Ação)** | `POST` | `/excluir/{id}` | Remove definitivamente o registro da notícia do banco de dados. | Público |

---

## Testes Automatizados (Padrão AAA)

A aplicação conta com uma suíte de testes rigorosa para garantir a qualidade do código e a confiabilidade das regras de negócio.

### 1. Testes Unitários (xUnit & Moq)
Desenvolvidos para as camadas de Domínio e Aplicação (foco no `NewsService`), os testes unitários seguem o padrão **AAA (Arrange, Act, Assert)** para garantir clareza e isolamento.
* **Framework**: xUnit.
* **Mocking**: Moq para simulação de dependências do repositório.
* **Nomenclatura**: Os métodos de teste seguem o padrão `Metodo_Cenario_ResultadoEsperado`.
* **Organização**: Localizados no projeto `News-Manager.Tests`, dentro da pasta `UnitTests`.

### 2. Testes de Integração
Estes testes validam o funcionamento dos endpoints da API de ponta a ponta, simulando o comportamento real do servidor.
* **WebApplicationFactory**: Utilizado para subir a aplicação em memória durante a execução dos testes.
* **Escopo**: Validação de fluxos HTTP, incluindo códigos de status (Success, NotFound), cabeçalhos de resposta e integridade do processamento de dados.
* **Organização**: Localizados no projeto `News-Manager.Tests`, dentro da pasta `IntegrationTests`.

### Como executar os testes

Para rodar toda a suíte de testes automatizados, certifique-se de estar na raiz da solução e execute o seguinte comando no terminal:

```bash
dotnet test
```

---

## Configurando a Conexão

O projeto espera uma conexão com o Oracle. Você deve configurar a string de conexão.
Edite o arquivo appsettings.json na raiz do projeto e substitua os valores:
```bash
"ConnectionStrings": {
  "OracleConnection": "Data Source=seu_datasource_oracle;User Id=seu_usuario;Password=sua_senha;"
}
```

---

## Como Rodar o Projeto

### Pré-requisitos
* [.NET SDK](https://dotnet.microsoft.com/download) instalado.
* Acesso a um banco de dados **Oracle**.
* Visual Studio 2022 ou VS Code.


#### 1. Clonando e entrando na pasta do projeto
Antes de executar os outros comandos, certifique-se de entrar na pasta da aplicação:
```bash
git clone https://github.com/jhonattalso/news-manager.git
cd News-Manager
```

#### 2. Restaurar as dependências
```bash
dotnet restore
```

#### 3. Instalar o Entity Framework CLI
```bash
dotnet tool install --global dotnet-ef
```
> Se já tiver instalado e quiser atualizar:
> ```bash
> dotnet tool update --global dotnet-ef
> ```


#### 4. Aplicando Migrations
Para criar as tabelas no banco de dados, execute o comando abaixo na raiz do projeto (onde está o arquivo .csproj):
```
dotnet ef database update
```

#### 5. Executando a Aplicação
Após configurar o banco, inicie o servidor apertando F5 ou abrindo o terminal e digitando (Lembre-se: Você deve estar dentro da pasta News-Manager):
```
dotnet run
```

