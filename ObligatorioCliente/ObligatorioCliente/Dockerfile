FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar todo el repositorio
COPY . .

# Compilar directamente el proyecto del cliente
WORKDIR "/src/ObligatorioCliente/ObligatorioCliente"
RUN dotnet publish "ObligatorioCliente.csproj" -c Release -o /app/publish

# Generar imagen final de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ObligatorioCliente.dll"]