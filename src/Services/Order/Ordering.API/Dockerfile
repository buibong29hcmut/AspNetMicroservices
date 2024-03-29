#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Order/Ordering.API/Ordering.API.csproj", "Services/Order/Ordering.API/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
COPY ["Services/Order/Ordering.Application/Ordering.Application.csproj", "Services/Order/Ordering.Application/"]
COPY ["Services/Order/Ordering.Domain/Ordering.Domain.csproj", "Services/Order/Ordering.Domain/"]
COPY ["Services/Order/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Services/Order/Ordering.Infrastructure/"]
RUN dotnet restore "Services/Order/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/Services/Order/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]