# StellarMinds

Sistema web para la gestión de una comunidad astronómica: administra usuarios, equipamiento, préstamos y observaciones, e incorpora inteligencia artificial para evaluar si una configuración de equipos es adecuada para observar un objeto celeste.

El proyecto nació como trabajo académico de **Desarrollo Web (2026)** y fue desarrollado de forma individual. Este repositorio reúne la solución completa: una API REST desplegada en Somee y un cliente web ASP.NET Core MVC que la consume.

## Demo y documentación

- **API desplegada:** [dwobligatoriofm.somee.com](http://www.dwobligatoriofm.somee.com)
- **Swagger / OpenAPI:** [dwobligatoriofm.somee.com/swagger](http://www.dwobligatoriofm.somee.com/swagger)
- **Cliente web:** actualmente se ejecuta de forma local desde Visual Studio o mediante la CLI de .NET.

> El hosting gratuito puede demorar unos segundos en responder durante la primera solicitud.

## Funcionalidades

La aplicación adapta las operaciones disponibles según el rol del usuario:

### Administrador

- Alta y consulta de socios, coordinadores y administradores.
- Gestión del inventario de telescopios, monturas, cámaras y oculares.
- Edición y baja de equipos, respetando sus relaciones y disponibilidad.
- Consulta de préstamos y de su historial de auditoría.
- Acceso al ranking de objetos celestes más observados.

### Coordinador

- Registro de préstamos de equipamiento a socios.
- Control de stock y de equipos disponibles.
- Registro de devoluciones.
- Consulta de socios con préstamos vigentes.

### Socio

- Consulta de sus préstamos y estados.
- Planificación y registro de observaciones.
- Evaluación de la combinación de equipos mediante Gemini, con un resultado —ideal, adecuado o no recomendable— y su explicación.
- Consulta del ranking de objetos celestes.

## Tecnologías

- C# y .NET 10
- ASP.NET Core Web API
- ASP.NET Core MVC y Razor Views
- Entity Framework Core
- SQL Server
- JWT para autenticación y autorización por roles
- Swagger / OpenAPI
- Bootstrap, CSS y JavaScript
- API de Google Gemini
- Somee para el despliegue de la API y la base de datos

## Arquitectura

La solución separa responsabilidades en capas y mantiene el dominio independiente de la infraestructura:

```text
Cliente MVC
    │ HTTP + JWT
    ▼
Web API (controllers y configuración)
    ▼
Casos de uso / Aplicación
    ▼
Dominio e interfaces
    ▲
Acceso a datos (EF Core, repositorios y Gemini)
    ▼
SQL Server / servicio externo
```

En el backend se aplican inyección de dependencias, repositorios, DTOs, mappers, casos de uso, value objects y excepciones de negocio. El modelo incluye herencia tanto para los tipos de usuario como para los distintos tipos de equipo.

## Estructura del repositorio

```text
.
├── ObligatorioDW2026/
│   ├── WebAPI/          # Endpoints REST, JWT, Swagger y configuración
│   ├── Aplicacion/      # Implementación de los casos de uso y mappers
│   ├── CasosUso/        # Contratos de casos de uso y DTOs
│   ├── Negocio/         # Entidades, value objects e interfaces
│   ├── AccesoDatos/     # EF Core, repositorios, migraciones y Gemini
│   └── Excepciones/     # Excepciones propias del dominio
└── ObligatorioCliente/
    ├── ObligatorioCliente/ # Aplicación MVC, vistas, DTOs y recursos estáticos
    └── Excepciones/         # Manejo de errores recibidos desde la API
```

## Ejecución local

### Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Visual Studio 2026 o un editor compatible con proyectos .NET
- SQL Server, únicamente si también se desea ejecutar la API localmente

### Opción rápida: cliente local + API desplegada

El cliente está configurado actualmente para utilizar la API publicada en Somee.

Desde una terminal en la raíz del repositorio:

```bash
dotnet run --project ObligatorioCliente/ObligatorioCliente/ObligatorioCliente.csproj
```

Luego abrir la URL indicada por la terminal (por defecto, `http://localhost:5077`). En Visual Studio también puede abrirse `ObligatorioCliente/ObligatorioCliente.slnx`, seleccionar el proyecto web y ejecutarlo.

### Solución completa en local

1. Configurar en `ObligatorioDW2026/WebAPI/appsettings.Development.json` una conexión SQL Server llamada `MiConexionDesarrollo`.
2. Configurar `Gemini:ApiKey` mediante secretos de usuario o variables de entorno.
3. Aplicar las migraciones desde el proyecto de acceso a datos:

   ```bash
   dotnet ef database update \
     --project ObligatorioDW2026/AccesoDatos/AccesoDatos.csproj \
     --startup-project ObligatorioDW2026/WebAPI/WebAPI.csproj
   ```

4. Ejecutar la API:

   ```bash
   dotnet run --project ObligatorioDW2026/WebAPI/WebAPI.csproj
   ```

5. Ajustar el cliente para usar las URLs de desarrollo definidas en su `appsettings.json` y ejecutarlo con el entorno `Development`.

## API

Los recursos principales expuestos son:

| Recurso | Responsabilidad |
| --- | --- |
| `/api/usuarios` | Autenticación y consulta de usuarios |
| `/api/socios` | Gestión de socios |
| `/api/coordinadores` | Gestión de coordinadores |
| `/api/administradores` | Alta de administradores |
| `/api/equipos` | Consulta y baja del inventario |
| `/api/telescopios` | Alta, edición y listado de telescopios |
| `/api/monturas` | Alta y edición de monturas |
| `/api/camaras` | Alta y edición de cámaras |
| `/api/oculares` | Alta y edición de oculares |
| `/api/prestamos` | Altas, consultas, devoluciones y auditoría |
| `/api/observaciones` | Evaluación y registro de observaciones |
| `/api/objetoscelestes` | Catálogo y ranking de objetos observados |

La mayoría de los endpoints requieren un token JWT y aplican autorización para los roles `Admin`, `Coordinador` o `Socio`. El detalle actualizado de rutas, cuerpos y respuestas puede consultarse en Swagger.

## Decisiones destacadas

- Separación entre dominio, aplicación, infraestructura y presentación.
- Reglas de negocio centralizadas en entidades y casos de uso.
- Autorización por rol tanto en la API como en la experiencia del cliente.
- Auditoría de las operaciones realizadas sobre préstamos.
- Evaluación con IA encapsulada detrás de una interfaz de servicio, evitando acoplar los casos de uso al proveedor externo.
- Persistencia con repositorios y migraciones de Entity Framework Core.

## Configuración y seguridad

Las credenciales, cadenas de conexión, claves JWT y claves de servicios externos no deberían quedar versionadas. Para desplegar o reutilizar el proyecto se recomienda suministrarlas mediante variables de entorno, secretos de usuario o el gestor de secretos del proveedor de hosting.

Ejemplos de claves de configuración esperadas:

```text
ConnectionStrings__MiConexionDesarrollo
ConnectionStrings__MiConexionProduccion
Gemini__ApiKey
```

## Estado del proyecto

Proyecto académico finalizado. La API se encuentra desplegada y operativa; el cliente web se ejecuta localmente y consume esa API remota.

