FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY ["src/webapi/webapi.csproj", "."]
COPY src/webapi .
RUN dotnet build "webapi.csproj" -c Release -o bin/build

FROM build AS publish
RUN dotnet publish "webapi.csproj" -c Release -o /app/bin/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/bin/publish .
ENTRYPOINT ["dotnet", "src/webapi/webapi.dll"]