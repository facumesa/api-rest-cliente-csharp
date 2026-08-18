# StellarMinds

Sistema web para la gestión de una comunidad astronómica. Permite administrar usuarios, equipamiento, préstamos y observaciones, e incorpora inteligencia artificial para evaluar si una configuración de equipos es adecuada para observar un objeto celeste.

El proyecto nació como trabajo académico de **Desarrollo Web (2026)** y fue desarrollado de forma individual. El repositorio contiene la solución completa: un cliente web ASP.NET Core MVC desplegado en Render y una API REST publicada en Somee.

## Demo

- **Aplicación web:** [stellarminds-web.onrender.com](https://stellarminds-web.onrender.com/)
- **API REST:** [dwobligatoriofm.somee.com](http://www.dwobligatoriofm.somee.com/)
- **Swagger / OpenAPI:** [dwobligatoriofm.somee.com/swagger](http://www.dwobligatoriofm.somee.com/swagger/)

> Los servicios utilizan planes gratuitos, por lo que la primera solicitud puede demorar unos segundos mientras se activa la instancia.

### Acceso a la Demo

Para probar las distintas funcionalidades de la plataforma, podés ingresar a [stellarminds-web.onrender.com](https://stellarminds-web.onrender.com/) con cualquiera de estas credenciales de prueba:

| Rol | Nombre de Usuario | Contraseña | Permisos principales |
|---|---|---|---|
| **Administrador** | `admindemo` | `Admin123!` | Gestión completa de usuarios, inventarios, auditoría y préstamos. |
| **Coordinador** | `coordemo` | `Coord123!` | Registro de préstamos, devoluciones y control de stock. |
| **Socio** | `sociodemo` | `Socio123!` | Solicitud de observaciones, evaluación con IA (Gemini) y ranking. |

## Funcionalidades

La interfaz y las operaciones disponibles se adaptan al rol de cada usuario.

### Administrador

- Alta y consulta de socios, coordinadores y administradores.
- Gestión de telescopios, monturas, cámaras y oculares.
- Edición y baja de equipos respetando sus relaciones y disponibilidad.
- Consulta de préstamos y de su historial de auditoría.
- Acceso al ranking de objetos celestes más observados.

### Coordinador

- Registro de préstamos de equipamiento a socios.
- Control de stock y equipos disponibles.
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
- Entity Framework Core y SQL Server
- JWT para autenticación y autorización por roles
- Swagger / OpenAPI
- Bootstrap, CSS y JavaScript
- API de Google Gemini
- Docker
- Render para el cliente web
- Somee para la API y la base de datos

## Arquitectura

La solución separa responsabilidades en capas y mantiene el dominio independiente de la infraestructura:

```text
Cliente MVC (Render)
        |
        | HTTP + JWT
        v
Web API (Somee)
        |
        v
Casos de uso / Aplicación
        |
        v
Dominio e interfaces
        ^
        |
Acceso a datos (EF Core, repositorios y Gemini)
        |
        v
SQL Server / servicio externo
```

En el backend se aplican inyección de dependencias, repositorios, DTOs, mappers, casos de uso, value objects y excepciones de negocio. El modelo incluye herencia tanto para los tipos de usuario como para los distintos tipos de equipo.

## Estructura del repositorio

```text
.
|-- ObligatorioDW2026/
|   |-- WebAPI/          # Endpoints REST, JWT, Swagger y configuración
|   |-- Aplicacion/      # Implementación de casos de uso y mappers
|   |-- CasosUso/        # Contratos de casos de uso y DTOs
|   |-- Negocio/         # Entidades, value objects e interfaces
|   |-- AccesoDatos/     # EF Core, repositorios, migraciones y Gemini
|   `-- Excepciones/     # Excepciones propias del dominio
`-- ObligatorioCliente/
    |-- ObligatorioCliente/ # Aplicación MVC, vistas, Dockerfile y estáticos
    `-- Excepciones/         # Manejo de errores recibidos desde la API
```

## Ejecución local

### Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Visual Studio o un editor compatible con proyectos .NET
- SQL Server, únicamente si se desea ejecutar también la API localmente

### Cliente local + API desplegada

El cliente está configurado para consumir la API publicada en Somee:

```bash
dotnet run --project ObligatorioCliente/ObligatorioCliente/ObligatorioCliente.csproj
```

Luego se debe abrir la URL indicada por la terminal, por defecto `http://localhost:5077`. Desde Visual Studio también puede abrirse `ObligatorioCliente/ObligatorioCliente.slnx`, seleccionar el proyecto web y ejecutarlo.

### Solución completa en local

1. Configurar en `ObligatorioDW2026/WebAPI/appsettings.Development.json` una conexión SQL Server llamada `MiConexionDesarrollo`.
2. Configurar `Gemini:ApiKey` mediante secretos de usuario o variables de entorno.
3. Aplicar las migraciones:

   ```bash
   dotnet ef database update \
     --project ObligatorioDW2026/AccesoDatos/AccesoDatos.csproj \
     --startup-project ObligatorioDW2026/WebAPI/WebAPI.csproj
   ```

4. Ejecutar la API:

   ```bash
   dotnet run --project ObligatorioDW2026/WebAPI/WebAPI.csproj
   ```

5. Configurar el cliente para utilizar las URLs de desarrollo y ejecutarlo con el entorno `Development`.

## Despliegue

El cliente MVC se publica en Render mediante el `Dockerfile` incluido en el proyecto. La imagen utiliza una etapa de compilación con el SDK de .NET 10 y una etapa final más liviana con el runtime de ASP.NET Core.

La API y SQL Server se encuentran alojados en Somee. El cliente desplegado se comunica con la API por HTTP y conserva el token JWT en la sesión del usuario para autorizar las solicitudes según su rol.

## API

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

La mayoría de los endpoints requieren un token JWT y aplican autorización para los roles `Admin`, `Coordinador` o `Socio`. El detalle actualizado de rutas, cuerpos y respuestas está disponible en Swagger.

## Decisiones destacadas

- Separación entre dominio, aplicación, infraestructura y presentación.
- Reglas de negocio centralizadas en entidades y casos de uso.
- Autorización por rol en la API y en la experiencia del cliente.
- Auditoría de operaciones realizadas sobre préstamos.
- Evaluación con IA encapsulada detrás de una interfaz de servicio.
- Persistencia mediante repositorios y migraciones de Entity Framework Core.
- Cliente contenerizado para lograr un despliegue reproducible.

## Configuración y seguridad

Las credenciales, cadenas de conexión y claves de servicios externos no se incluyen en el repositorio. Para ejecutar o desplegar el proyecto deben suministrarse mediante secretos de usuario, variables de entorno o la configuración privada del proveedor de hosting.

Ejemplos de claves esperadas:

```text
ConnectionStrings__MiConexionDesarrollo
ConnectionStrings__MiConexionProduccion
Gemini__ApiKey
```

## Estado del proyecto

Proyecto académico finalizado y desplegado. Tanto el cliente web como la API se encuentran disponibles públicamente para demostración.
