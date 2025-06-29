<!-- In README.md -->
[Português 🇧🇷](README.pt-BR.md)

---

# FeatureTracker

FeatureTracker is a modern web application for managing and tracking new features, built with Blazor WebAssembly (.NET 9) and a secure RESTful backend. The system offers JWT authentication, SQL Server integration, real-time notifications, and a powerful UI based on MudBlazor.

### Overview

- **Frontend:** Blazor WebAssembly (.NET 9, C#)
- **Backend:** ASP.NET Core RESTful API
- **UI Framework:** MudBlazor
- **Database:** SQL Server
- **Authentication:** JWT (JSON Web Token)
- **Documentation:** Swagger/OpenAPI & Scalar
- **PWA:** Offline support via Service Worker

### Features

- User registration and authentication (JWT)
- Password recovery via email (MailKit)
- Real-time interface notifications
- Feature and roadmap management
- Third-party integrations via API
- Advanced search and filtering
- Modern, responsive UI
- **AI-powered feature title suggestions** (GeminiService)

### Main Endpoints

#### Authentication

- `POST /api/v1/Auth/Login` — User login, returns JWT token
- `POST /api/v1/Auth/Register` — Register new user

#### Other Resources

- Real-time notifications
- Secure password recovery link (expires in 24h)
- Interactive documentation: `/swagger` and `/scalar`

### Status Codes

- **200**: Success
- **400**: Bad request
- **401**: Unauthorized
- **403**: Forbidden
- **404**: Not found
- **500**: Internal server error

### Security

- All requests must use HTTPS
- JWT tokens have configurable expiration
- Sensitive data encrypted
- Rate limiting to prevent abuse

### Getting Started Locally

1. Clone this repository
2. Configure the SQL Server connection string in `appsettings.json`
3. (Optional) Run Entity Framework migrations
4. Start the project via Visual Studio or run `dotnet run` in the `FeatureTracker.Server` folder
5. Access the app at `https://localhost:<port>`

### Technologies Used

- .NET 9 (Blazor WebAssembly, ASP.NET Core)
- MudBlazor
- SQL Server
- JWT
- MailKit
- Swagger/OpenAPI, Scalar

### More Documentation

See [ABOUT.md](ABOUT.md) for a detailed API overview, authentication flows, error handling, and advanced usage.

---

> Developed with ❤️ by [Gabriel Carrijo](https://github.com/carrijoga)

---
