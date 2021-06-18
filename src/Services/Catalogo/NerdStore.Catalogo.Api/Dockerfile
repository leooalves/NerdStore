#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Catalogo/NerdStore.Catalogo.Api/NerdStore.Catalogo.Api.csproj", "src/Services/Catalogo/NerdStore.Catalogo.Api/"]
COPY ["src/Services/Catalogo/NerdStore.Catalogo.Domain/NerdStore.Catalogo.Domain.csproj", "src/Services/Catalogo/NerdStore.Catalogo.Domain/"]
COPY ["src/Services/Shared/NerdStore.Shared/NerdStore.Shared.csproj", "src/Services/Shared/NerdStore.Shared/"]
COPY ["src/Services/Catalogo/NerdStore.Catalogo.Infra/NerdStore.Catalogo.Infra.csproj", "src/Services/Catalogo/NerdStore.Catalogo.Infra/"]
COPY ["src/Services/Catalogo/NerdStore.Catalogo.Application/NerdStore.Catalogo.Application.csproj", "src/Services/Catalogo/NerdStore.Catalogo.Application/"]
RUN dotnet restore "src/Services/Catalogo/NerdStore.Catalogo.Api/NerdStore.Catalogo.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalogo/NerdStore.Catalogo.Api"
RUN dotnet build "NerdStore.Catalogo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NerdStore.Catalogo.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NerdStore.Catalogo.Api.dll"]