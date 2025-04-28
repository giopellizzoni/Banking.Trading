FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Banking.Trading.API/Banking.Trading.API.csproj", "src/Banking.Trading.API/"]
COPY ["src/Banking.Trading.Infrastructure/Banking.Trading.Infrastructure.csproj", "src/Banking.Trading.Infrastructure/"]
COPY ["src/Banking.Trading.Domain/Banking.Trading.Domain.csproj", "src/Banking.Trading.Domain/"]
COPY ["src/Banking.Trading.Application/Banking.Trading.Application.csproj", "src/Banking.Trading.Application/"]
RUN dotnet restore "src/Banking.Trading.API/Banking.Trading.API.csproj"
COPY . .
WORKDIR "/src/src/Banking.Trading.API"
RUN dotnet build "./Banking.Trading.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Banking.Trading.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Banking.Trading.API.dll"]
