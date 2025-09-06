FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7138

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ParkingApp2.sln", "."]
COPY ["Backend/API/API.csproj", "Backend/API/"]
COPY ["Backend/Business/Business.csproj", "Backend/Business/"]
COPY ["Backend/Data/Data.csproj", "Backend/Data/"]
COPY ["Backend/Models/Models.csproj", "Backend/Models/"]

RUN dotnet restore "ParkingApp2.sln"
COPY . .
WORKDIR "/src"
RUN dotnet build "ParkingApp2.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ParkingApp2.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]