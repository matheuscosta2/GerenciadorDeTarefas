   # Gerenciador de Tarefas

   Este é um projeto de Gerenciador de Tarefas desenvolvido com Angular no frontend e .NET 8 no backend.

   ## Funcionalidades

   - Listagem de tarefas
   - Criação de novas tarefas
   - Edição de tarefas existentes
   - Exclusão de tarefas
   - Autenticação de usuários

   ## Tecnologias Utilizadas

   - Angular
   - .NET 8
   - SQLite
   - Bootstrap

   ## Configuração do Projeto

   ### Frontend

   Para configurar o frontend, siga os passos abaixo:

   1. Navegue até a pasta do frontend:
       cd gerenciador-de-tarefas-frontend
   2. Instale as dependências:
       npm install
   
   3. Inicie o servidor de desenvolvimento:
       npm start

   
   ### Backend

   Para configurar o backend, siga os passos abaixo:

   1. Navegue até a pasta do backend:
      cd GerenciadorDeTarefas
   2. Configure a conexão com o banco de dados SQLite no arquivo `appsettings.json`:
       
    {
    "ConnectionStrings": {
      "DefaultConnection": "Data Source=gerenciador-de-tarefas.db"
    }
    }

   3. Execute as migrações para criar o banco de dados:
      dotnet ef database update

   4. Inicie o servidor:
      dotnet run

   
   - Abra o navegador e acesse a url gerada pelo npm start

   Só será possível logar utilizar as funcionalidades da aplicação estando logado, o login e senha estão estaticos no código propositadamente à fim de facilitar a execução, as considerações sobre 
   as boas praticas e melhorias estão logo abaixo.

## Decisões Técnicas

### Arquitetura Utilizada

O projeto segue uma arquitetura em camadas, dividida em:

- **Domain:** Contém as entidades e interfaces de repositório.
- **Application:** Contém os serviços de aplicação e DTOs.
- **Infrastructure:** Contém a implementação dos repositórios e a configuração do Entity Framework Core.
- **API:** Contém os controladores e a configuração do ASP.NET Core.

### Estrutura de Pastas

- **GerenciadorDeTarefas.Domain:** Contém as entidades e interfaces de repositório.
- **GerenciadorDeTarefas.Application:** Contém os serviços de aplicação e DTOs.
- **GerenciadorDeTarefas.Infrastructure:** Contém a implementação dos repositórios e a configuração do Entity Framework Core.
- **GerenciadorDeTarefas:** Contém os controladores e a configuração do ASP.NET Core.
- **gerenciador-de-tarefas-frontend:** Contém o frontend desenvolvido em Angular.

### Testes Unitários

Os testes unitários foram implementados utilizando o framework xUnit para o backend. Os testes cobrem os serviços de aplicação e os controladores.

### Implementação do JWT

A autenticação JWT foi configurada no projeto utilizando o pacote `Microsoft.AspNetCore.Authentication.JwtBearer`. As chaves e configurações de autenticação estão definidas no arquivo `appsettings.json`.

### Informações Sensíveis

Para facilitar a execução do projeto, algumas informações como usuário e senha estão expostas no código. No entanto, é importante seguir as melhores práticas para lidar com informações sensíveis:

- Utilize o `dotnet user-secrets` para armazenar informações sensíveis durante o desenvolvimento.
- Em ambientes de produção, utilize variáveis de ambiente ou serviços de gerenciamento de segredos como Azure Key Vault.

## Possíveis Melhorias

- **Implementação de CI/CD:** Configurar pipelines de integração e entrega contínua utilizando GitHub Actions ou Azure DevOps.
- **Melhoria na Segurança:** Implementar políticas de segurança mais robustas, como a rotação de chaves e a utilização de HTTPS.
- **Testes de Integração:** Adicionar testes de integração para garantir que todos os componentes do sistema funcionem corretamente juntos.
- **Melhoria na UI:** Melhorar a interface do usuário do frontend para uma melhor experiência do usuário.
- **Melhoria na arquitetura:** Pode ser integrado o padrão de CQRS e commands para apartar as lógicas e dominios, mmelhorar a performance e escalabilidade da API.
    

  
  
