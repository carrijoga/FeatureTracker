# Documentação da API CarCare

## Visão Geral
A API CarCare é uma aplicação RESTful desenvolvida em .NET que fornece endpoints para gerenciamento de serviços automotivos.

## Autenticação
A API utiliza autenticação JWT (JSON Web Token) para proteger os endpoints. Para acessar endpoints protegidos, é necessário incluir o token JWT no cabeçalho da requisição:

```
Authorization: Bearer <seu_token_jwt>
```

## Endpoints

### Autenticação

#### POST /api/auth/login
Realiza o login do usuário e retorna um token JWT.

**Request Body:**
```json
{
    "email": "string",
    "password": "string"
}
```

**Response (200 OK):**
```json
{
    "token": "string",
    "expiration": "datetime",
    "user": {
        "id": "string",
        "email": "string",
        "name": "string"
    }
}
```

#### POST /api/auth/register
Registra um novo usuário no sistema.

**Request Body:**
```json
{
    "email": "string",
    "password": "string",
    "name": "string"
}
```

**Response (200 OK):**
```json
{
    "id": "string",
    "email": "string",
    "name": "string"
}
```

## Códigos de Status

- 200: Sucesso
- 400: Requisição inválida
- 401: Não autorizado
- 403: Acesso negado
- 404: Recurso não encontrado
- 500: Erro interno do servidor

## Tratamento de Erros
A API retorna mensagens de erro no seguinte formato:

```json
{
    "message": "string",
    "errors": [
        {
            "field": "string",
            "message": "string"
        }
    ]
}
```

## Limitações e Considerações

- O token JWT expira após um período determinado
- Todas as requisições devem ser feitas via HTTPS
- Os dados sensíveis são criptografados antes de serem armazenados
- A API implementa rate limiting para prevenir abusos

## Suporte
Para suporte técnico ou dúvidas, entre em contato através do email: suporte@carcare.com