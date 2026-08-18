FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar las referencias de proyectos para restaurar dependencias
COPY ["ObligatorioCliente/ObligatorioCliente/ObligatorioCliente.csproj", "ObligatorioCliente/ObligatorioCliente/"]
COPY ["Excepciones/Excepciones.csproj", "Excepciones/"]

RUN dotnet restore "ObligatorioCliente/ObligatorioCliente/ObligatorioCliente.csproj"

# Copiar todo el repositorio
COPY . .

WORKDIR "/src/ObligatorioCliente/ObligatorioCliente"
RUN dotnet publish "ObligatorioCliente.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ObligatorioCliente.dll"]