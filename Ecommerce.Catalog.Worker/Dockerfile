#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ecommerce.Catalog.Worker/Ecommerce.Catalog.Worker.csproj", "Ecommerce.Catalog.Worker/"]
COPY ["Ecommerce.Catalog.Messaging/Ecommerce.Catalog.Messaging.csproj", "Ecommerce.Catalog.Messaging/"]
RUN dotnet restore "Ecommerce.Catalog.Worker/Ecommerce.Catalog.Worker.csproj"
COPY . .
WORKDIR "/src/Ecommerce.Catalog.Worker"
RUN dotnet build "Ecommerce.Catalog.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Catalog.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Catalog.Worker.dll"]
