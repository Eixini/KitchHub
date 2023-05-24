#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /
COPY ["webapi.csproj", "/"]
RUN dotnet restore "webapi.csproj"
COPY . .
WORKDIR "/"
RUN dotnet build "webapi.csproj" -c Release -o /bin/build

FROM build AS publish
RUN dotnet publish "webapi.csproj" -c Release -o /bin/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /
COPY --from=publish /bin/publish .
ENTRYPOINT ["dotnet", "webapi.dll"]