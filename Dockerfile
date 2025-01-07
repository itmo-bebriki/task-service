FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ./*.props ./

COPY ["src/Itmo.Bebriki.Tasks/Itmo.Bebriki.Tasks.csproj", "src/Itmo.Bebriki.Tasks/"]

COPY ["src/Application/Itmo.Bebriki.Tasks.Application/Itmo.Bebriki.Tasks.Application.csproj", "src/Application/Itmo.Bebriki.Tasks.Application/"]
COPY ["src/Application/Itmo.Bebriki.Tasks.Application.Abstractions/Itmo.Bebriki.Tasks.Application.Abstractions.csproj", "src/Application/Itmo.Bebriki.Tasks.Application.Abstractions/"]
COPY ["src/Application/Itmo.Bebriki.Tasks.Application.Contracts/Itmo.Bebriki.Tasks.Application.Contracts.csproj", "src/Application/Itmo.Bebriki.Tasks.Application.Contracts/"]
COPY ["src/Application/Itmo.Bebriki.Tasks.Application.Models/Itmo.Bebriki.Tasks.Application.Models.csproj", "src/Application/Itmo.Bebriki.Tasks.Application.Models/"]

COPY ["src/Presentation/Itmo.Bebriki.Tasks.Presentation.Kafka/Itmo.Bebriki.Tasks.Presentation.Kafka.csproj", "src/Presentation/Itmo.Bebriki.Tasks.Presentation.Kafka/"]
COPY ["src/Presentation/Itmo.Bebriki.Tasks.Presentation.Grpc/Itmo.Bebriki.Tasks.Presentation.Grpc.csproj", "src/Presentation/Itmo.Bebriki.Tasks.Presentation.Grpc/"]

COPY ["src/Infrastructure/Itmo.Bebriki.Tasks.Infrastructure.Persistence/Itmo.Bebriki.Tasks.Infrastructure.Persistence.csproj", "src/Infrastructure/Itmo.Bebriki.Tasks.Infrastructure.Persistence/"]

RUN dotnet restore "src/Itmo.Bebriki.Tasks/Itmo.Bebriki.Tasks.csproj"

COPY . .
WORKDIR "/src/src/Itmo.Bebriki.Tasks"
RUN dotnet build "Itmo.Bebriki.Tasks.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Itmo.Bebriki.Tasks.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Itmo.Bebriki.Tasks.dll"]