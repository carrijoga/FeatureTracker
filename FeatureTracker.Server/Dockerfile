# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["FeatureTracker.Server/FeatureTracker.Server.csproj", "FeatureTracker.Server/"]
COPY ["FeatureTracker.Application/FeatureTracker.Application.csproj", "FeatureTracker.Application/"]
COPY ["FeatureTracker.Infrastructure/FeatureTracker.Infrastructure.csproj", "FeatureTracker.Infrastructure/"]
COPY ["FeatureTracker.Domain/FeatureTracker.Domain.csproj", "FeatureTracker.Domain/"]
COPY ["FeatureTracker.Shared/FeatureTracker.Shared.csproj", "FeatureTracker.Shared/"]
COPY ["FeatureTracker/FeatureTracker.Client.csproj", "FeatureTracker/"]
RUN dotnet restore "./FeatureTracker.Server/FeatureTracker.Server.csproj"
COPY . .
WORKDIR "/src/FeatureTracker.Server"
RUN dotnet build "./FeatureTracker.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FeatureTracker.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FeatureTracker.Server.dll"]