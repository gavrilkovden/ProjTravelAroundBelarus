#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Users/Users.Api.csproj", "Users/"]
COPY ["Core.Api/Core.Api.csproj", "Core.Api/"]
COPY ["Core.Application/Core.Application.csproj", "Core.Application/"]
COPY ["Core.Auth.Api/Core.Auth.Api.csproj", "Core.Auth.Api/"]
COPY ["Core.Auth.Application/Core.Auth.Application.csproj", "Core.Auth.Application/"]
COPY ["Core.Users.Domain/Core.Users.Domain.csproj", "Core.Users.Domain/"]
COPY ["Infrastructure.Persistence/Infrastructure.Persistence.csproj", "Infrastructure.Persistence/"]
COPY ["Auth.Domain/Auth.Domain.csproj", "Auth.Domain/"]
COPY ["Travels.Domain/Travels.Domain.csproj", "Travels.Domain/"]
COPY ["Users.Application/Users.Application.csproj", "Users.Application/"]
RUN dotnet restore "./Users/Users.Api.csproj"
COPY . .
WORKDIR "/src/Users"
RUN dotnet build "./Users.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Users.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.Api.dll"]