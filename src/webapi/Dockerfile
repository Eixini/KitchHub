FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /usr/src/kitchhub
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /usr/src/kitchhub
COPY ["src/webapi/webapi.csproj", "."]
RUN dotnet restore "webapi.csproj"
COPY src/webapi .
WORKDIR .
RUN dotnet build "webapi.csproj" -c Release -o bin/build

FROM build AS publish
RUN dotnet publish "webapi.csproj" -c Release -o src/webapi/bin/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /usr/lib/kitchhub
COPY --from=publish /usr/src/kitchhub/src/webapi/bin/publish .
ENTRYPOINT ["dotnet", "src/webapi/webapi.dll"]