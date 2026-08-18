FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar el archivo de proyecto y restaurar dependencias
COPY ["ObligatorioCliente.csproj", "./"]
RUN dotnet restore "ObligatorioCliente.csproj"

# Copiar todo el código y compilar
COPY . .
RUN dotnet publish "ObligatorioCliente.csproj" -c Release -o /app/publish

# Generar la imagen final para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ObligatorioCliente.dll"]