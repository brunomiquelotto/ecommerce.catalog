#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ecommerce.Catalog/Ecommerce.Catalog.csproj", "Ecommerce.Catalog/"]
RUN dotnet restore "Ecommerce.Catalog/Ecommerce.Catalog.csproj"
COPY . .
WORKDIR "/src/Ecommerce.Catalog"
RUN dotnet build "Ecommerce.Catalog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Catalog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Catalog.dll"]
