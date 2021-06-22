#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Vendas/NerdStore.Vendas.Api/NerdStore.Vendas.Api.csproj", "src/Services/Vendas/NerdStore.Vendas.Api/"]
COPY ["src/Services/Vendas/NerdStore.Vendas.Domain/NerdStore.Vendas.Domain.csproj", "src/Services/Vendas/NerdStore.Vendas.Domain/"]
COPY ["src/Services/Shared/NerdStore.Shared/NerdStore.Shared.csproj", "src/Services/Shared/NerdStore.Shared/"]
COPY ["src/Services/Vendas/NerdStore.Vendas.Infra/NerdStore.Vendas.Infra.csproj", "src/Services/Vendas/NerdStore.Vendas.Infra/"]
RUN dotnet restore "src/Services/Vendas/NerdStore.Vendas.Api/NerdStore.Vendas.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Vendas/NerdStore.Vendas.Api"
RUN dotnet build "NerdStore.Vendas.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NerdStore.Vendas.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NerdStore.Vendas.Api.dll"]