#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["eAgendaMedica.WebAPI/eAgendaMedica.WebAPI.csproj", "eAgendaMedica.WebAPI/"]
COPY ["eAgenda.Dominio/eAgendaMedica.Dominio.csproj", "eAgenda.Dominio/"]
COPY ["eAgenda.Infra.Orm/eAgendaMedica.Infra.Orm.csproj", "eAgenda.Infra.Orm/"]
COPY ["eAgendaMedica.Aplicacao/eAgendaMedica.Aplicacao.csproj", "eAgendaMedica.Aplicacao/"]
RUN dotnet restore "eAgendaMedica.WebAPI/eAgendaMedica.WebAPI.csproj"
COPY . .
WORKDIR "/src/eAgendaMedica.WebAPI"
RUN dotnet build "eAgendaMedica.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eAgendaMedica.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eAgendaMedica.WebAPI.dll"]