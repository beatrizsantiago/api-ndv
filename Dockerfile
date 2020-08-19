FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY api-ndv.sln ./
COPY Domain/*.csproj ./Domain/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY Repository/*.csproj ./Repository/
COPY WebApplication/*.csproj ./WebApplication/

RUN dotnet restore
COPY . .

WORKDIR /src/Domain
RUN dotnet build -c Release -o /app

WORKDIR /src/Infrastructure
RUN dotnet build -c Release -o /app

WORKDIR /src/Repository
RUN dotnet build -c Release -o /app

WORKDIR /src/WebApplication
RUN find -type d -name bin -prune -exec rm -rf {} \; && find -type d -name obj -prune -exec rm -rf {} \;
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app .
RUN cp /usr/share/zoneinfo/America/Fortaleza /etc/localtime
ENTRYPOINT ["dotnet", "WebApplication.dll"]