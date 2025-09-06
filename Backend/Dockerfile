# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todo el código
COPY . .

# Restaurar todos los paquetes desde la solución
RUN dotnet restore ParkingApp2.sln

# Compilar y publicar la API
RUN dotnet build Backend/API/API.csproj -c Release
RUN dotnet publish Backend/API/API.csproj -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 7138
ENTRYPOINT ["dotnet", "API.dll"]
