#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Karonda.ModbusTcp.Server/Karonda.ModbusTcp.Server.csproj", "Karonda.ModbusTcp.Server/"]
COPY ["Karonda.ModbusTcp/Karonda.ModbusTcp.csproj", "Karonda.ModbusTcp/"]
RUN dotnet restore "Karonda.ModbusTcp.Server/Karonda.ModbusTcp.Server.csproj"
COPY . .
WORKDIR "/src/Karonda.ModbusTcp.Server"
RUN dotnet build "Karonda.ModbusTcp.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Karonda.ModbusTcp.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Karonda.ModbusTcp.Server.dll"]