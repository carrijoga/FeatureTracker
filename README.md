# FeatureTracker

FeatureTracker é uma aplicação web moderna para gerenciamento e rastreamento de novas funcionalidades, desenvolvida com Blazor WebAssembly (.NET 9) e backend RESTful seguro. O sistema oferece autenticação JWT, integração com SQL Server, notificações em tempo real e uma interface avançada baseada em MudBlazor.

## Visão Geral
- **Frontend:** Blazor WebAssembly (C#/.NET 9)
- **Backend:** ASP.NET Core RESTful API
- **UI:** MudBlazor
- **Banco de Dados:** SQL Server
- **Autenticação:** JWT (JSON Web Token)
- **Documentação:** Swagger/OpenAPI e Scalar
- **PWA:** Suporte offline via Service Worker

## Funcionalidades
- Cadastro e autenticação de usuários (JWT)
- Recuperação de senha por e-mail (MailKit)
- Notificações em tempo real na interface
- Gerenciamento de funcionalidades e roadmap
- Integração com ferramentas de terceiros via API
- Filtros avançados de busca
- Interface responsiva e moderna

## Endpoints Principais
### Autenticação
- `POST /api/v1/Auth/Login` — Login de usuário, retorna token JWT
- `POST /api/v1/Auth/Register` — Cadastro de novo usuário

### Outros Recursos
- Notificações em tempo real
- Recuperação de senha com link seguro (expira em 24h)
- Documentação interativa: `/swagger` e `/scalar`

## Códigos de Status
- 200: Sucesso
- 400: Requisição inválida
- 401: Não autorizado
- 403: Acesso negado
- 404: Recurso não encontrado
- 500: Erro interno do servidor

## Segurança
- Todas as requisições devem ser feitas via HTTPS
- Tokens JWT expiram após período determinado
- Dados sensíveis criptografados
- Rate limiting para evitar abusos

## Como rodar localmente
1. Clone o repositório
2. Configure a string de conexão do SQL Server em `appsettings.json`
3. Execute as migrações do Entity Framework (opcional)
4. Rode o projeto via Visual Studio ou `dotnet run` na pasta `FeatureTracker.Server`
5. Acesse `https://localhost:<porta>`

## Tecnologias Utilizadas
- .NET 9 (Blazor WebAssembly, ASP.NET Core)
- MudBlazor
- SQL Server
- JWT
- MailKit
- Swagger/OpenAPI, Scalar

---

> Desenvolvido com ❤️ por [Gabriel Carrijo](https://github.com/carrijoga)
