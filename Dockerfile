FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Копируем файлы решений и проектов
COPY *.sln ./
COPY TravelAroundBelarusProj/*.csproj ./TravelAroundBelarusProj/
COPY Core.Auth.Application/*.csproj ./Core.Auth.Application/
COPY Core.Users.Domain/*.csproj ./Core.Users.Domain/
COPY Users/*.csproj ./Users/
COPY Auth/*.csproj ./Auth/
COPY Core.ArchitectureTests/*.csproj ./Core.ArchitectureTests/
COPY Auth.UnitTests/*.csproj ./Auth.UnitTests/
COPY Attractions.Application/*.csproj ./Attractions.Application/
COPY Auth.Application/*.csproj ./Auth.Application/
COPY Auth.Domain/*.csproj ./Auth.Domain/
COPY Core.Api/*.csproj ./Core.Api/
COPY Core.Application/*.csproj ./Core.Application/
COPY Core.Auth.Api/*.csproj ./Core.Auth.Api/
COPY Core.Tests/*.csproj ./Core.Tests/
COPY Infrastructure.Persistence/*.csproj ./Infrastructure.Persistence/
COPY Routes.Application/*.csproj ./Routes.Application/
COPY Tours.Application/*.csproj ./Tours.Application/
COPY Travels.Domain/*.csproj ./Travels.Domain/
COPY Travels.UnitTests/*.csproj ./Travels.UnitTests/
COPY Users.Application/*.csproj ./Users.Application/
COPY Users.UnitTests/*.csproj ./Users.UnitTests/

# Выполняем восстановление зависимостей
RUN dotnet restore

# Копируем остальные файлы и выполняем сборку
COPY . .
RUN dotnet build -c Release -o /app/build

# Этап runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Установка необходимых утилит (curl и ping)
RUN apt-get update && apt-get install -y curl iputils-ping

# Копируем собранные файлы из этапа сборки
COPY --from=build /app/build .

ENTRYPOINT ["dotnet", "TravelAroundBelarusProj.Api.dll"]