#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CodeMasters.Api/CodeMasters.Api.csproj", "CodeMasters.Api/"]
COPY ["CodeMasters.Domain/CodeMasters.Domain.csproj", "CodeMasters.Domain/"]
COPY ["CodeMasters.Infrastructure/CodeMasters.Infrastructure.csproj", "CodeMasters.Infrastructure/"]
RUN dotnet restore "CodeMasters.Api/CodeMasters.Api.csproj"
COPY . .
WORKDIR "/src/CodeMasters.Api"
RUN dotnet build "CodeMasters.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodeMasters.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodeMasters.Api.dll"]