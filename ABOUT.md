# FeatureTracker - Documentação da API

## Visão Geral

O FeatureTracker é uma aplicação web desenvolvida em Blazor WebAssembly (.NET 9) para gerenciamento e rastreamento de funcionalidades, com backend RESTful seguro e moderno. O sistema oferece autenticação JWT, integração com banco de dados SQL Server, notificações em tempo real e recursos avançados de UI utilizando MudBlazor.

---

## Autenticação

A API utiliza autenticação baseada em JWT (JSON Web Token). Para acessar endpoints protegidos, inclua o token no cabeçalho da requisição:

---

## Endpoints Principais

### Autenticação

#### POST /api/v1/Auth/Login

Realiza o login do usuário e retorna um token JWT.

**Request Body:**

**Response (200 OK):**

#### POST /api/v1/Auth/Register

Registra um novo usuário no sistema.

**Request Body:**

**Response (200 OK):**

---

## Notificações

O sistema possui um componente de notificações em tempo real na interface, utilizando MudBlazor, para alertar usuários sobre eventos importantes.

---

## Recuperação de Senha

Usuários podem solicitar recuperação de senha. Um e-mail personalizado é enviado com link seguro para redefinição, válido por 24 horas.

---

## Códigos de Status

- 200: Sucesso
- 400: Requisição inválida
- 401: Não autorizado
- 403: Acesso negado
- 404: Recurso não encontrado
- 500: Erro interno do servidor

---

## Tratamento de Erros

As respostas de erro seguem o padrão:

---

## Tecnologias Utilizadas

- .NET 9 (Blazor WebAssembly e ASP.NET Core)
- MudBlazor (UI)
- SQL Server (persistência)
- JWT (autenticação)
- Scalar e Swagger/OpenAPI (documentação)
- MailKit (envio de e-mails)
- Service Worker (PWA/offline)

---

## Considerações e Segurança

- O token JWT expira após determinado período.
- Todas as requisições devem ser feitas via HTTPS.
- Dados sensíveis são criptografados.
- Implementação de rate limiting para evitar abusos.
- Suporte a PWA para funcionamento offline.

---

## Documentação Interativa

Acesse a documentação interativa da API via Scalar ou Swagger em ambiente de desenvolvimento:

- `/swagger`
- `/scalar`

---

## Suporte

Dúvidas ou suporte técnico: suporte@featuretracker.com

---
