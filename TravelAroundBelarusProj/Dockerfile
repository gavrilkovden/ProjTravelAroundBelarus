#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TravelAroundBelarusProj/TravelAroundBelarusProj.Api.csproj", "TravelAroundBelarusProj/"]
COPY ["Attractions.Application/Attractions.Application.csproj", "Attractions.Application/"]
COPY ["Auth.Application/Auth.Application.csproj", "Auth.Application/"]
COPY ["Auth.Domain/Auth.Domain.csproj", "Auth.Domain/"]
COPY ["Core.Users.Domain/Core.Users.Domain.csproj", "Core.Users.Domain/"]
COPY ["Core.Application/Core.Application.csproj", "Core.Application/"]
COPY ["Core.Auth.Application/Core.Auth.Application.csproj", "Core.Auth.Application/"]
COPY ["Travels.Domain/Travels.Domain.csproj", "Travels.Domain/"]
COPY ["Core.Api/Core.Api.csproj", "Core.Api/"]
COPY ["Core.Auth.Api/Core.Auth.Api.csproj", "Core.Auth.Api/"]
COPY ["Infrastructure.Persistence/Infrastructure.Persistence.csproj", "Infrastructure.Persistence/"]
COPY ["Routes.Application/Routes.Application.csproj", "Routes.Application/"]
COPY ["Tours.Application/Tours.Application.csproj", "Tours.Application/"]
COPY ["Users/Users.Api.csproj", "Users/"]
COPY ["Users.Application/Users.Application.csproj", "Users.Application/"]
RUN dotnet restore "./TravelAroundBelarusProj/TravelAroundBelarusProj.Api.csproj"
COPY . .
WORKDIR "/src/TravelAroundBelarusProj"
RUN dotnet build "./TravelAroundBelarusProj.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TravelAroundBelarusProj.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TravelAroundBelarusProj.Api.dll"]