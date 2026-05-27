# Guía de Configuración y Ejecución del Proyecto Web API .NET 10

## Descripción

Requisitos y pasos necesarios para configurar y ejecutar localmente la solución Web API desarrollada en .NET 10 utilizando Visual Studio y SQL Server LocalDB.

---

# Requisitos Previos

Antes de ejecutar el proyecto, asegúrese de tener instalado lo siguiente:

## 1. Visual Studio 2026

Se recomienda utilizar la versión más reciente de Visual Studio 2026.

### Workloads requeridos

- ASP.NET and web development
- .NET desktop development

---

## 2. SDK de .NET 10

Instalar el SDK de .NET 10 correspondiente.

Puede verificar la instalación ejecutando:

```bash
dotnet --version
```

---

## 3. SQL Server LocalDB

El proyecto utiliza SQL Server LocalDB como base de datos local.

La instalación normalmente viene incluida con:

- Visual Studio
- SQL Server Express

Puede verificar que LocalDB esté instalado ejecutando:

```bash
sqllocaldb info
```

---

## 4. SQL Server Management Studio (Opcional)

Herramienta recomendada para visualizar la base de datos y validar tablas, usuarios y datos.

---

# Estructura de la Solución

La solución está organizada utilizando una arquitectura limpia (Clean Architecture) con las siguientes capas:

| Proyecto | Responsabilidad |
|---|---|
| WebAPI | Capa de presentación / endpoints |
| Application | Lógica de aplicación |
| Infrastructure | Persistencia, Entity Framework, Identity, JWT |
| Domain | Entidades y reglas de dominio |

---

# Configuración Inicial

## 1. Clonar el repositorio

```bash
git clone https://github.com/EbarriosCode/WBAPI
```

---

## 2. Abrir la solución

Abrir el archivo `.sln` utilizando Visual Studio.

---

## 3. Restaurar paquetes NuGet

Visual Studio normalmente restaura los paquetes automáticamente.

En caso contrario ejecutar:

```bash
dotnet restore
```

---

# Configuración de Base de Datos

## 1. Validar cadena de conexión

Verificar el archivo:

```text
appsettings.Development.json
```

Ejemplo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AuthDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
---

# Nota sobre Configuración y Seguridad

Para facilitar la evaluación técnica y permitir la ejecución inmediata del proyecto, se incluyó temporalmente una cadena de conexión local en el archivo:

```text
appsettings.Development.json
```

La base de datos utilizada corresponde a una instancia local de SQL Server LocalDB y no expone credenciales de producción ni recursos externos.

Esta configuración fue agregada únicamente con fines demostrativos para simplificar la revisión del código y la ejecución del proyecto durante el proceso técnico.

## Buenas Prácticas

En mi experiencia en entornos reales y productivos no es recomendable almacenar secretos o cadenas de conexión directamente en archivos de configuración versionados en el repositorio.

En escenarios profesionales normalmente se utilizan mecanismos más seguros como:

- `secrets.json` mediante User Secrets en desarrollo local
- Variables de entorno
- Azure Key Vault
- AWS Secrets Manager
- AWS Systems Manager Parameter Store
- HashiCorp Vault
- Kubernetes Secrets
- Docker Secrets
- Managed Identities / IAM Roles para evitar credenciales hardcodeadas

## Recomendación para Producción

Para ambientes productivos, las credenciales y secretos deben administrarse mediante servicios especializados de gestión de secretos y rotación segura de credenciales, evitando exponer información sensible en el código fuente o repositorios Git.


## 2. Ejecutar Migraciones

El proyecto utiliza:

- Entity Framework Core
- ASP.NET Identity
- JWT Authentication

Las tablas de usuarios y autenticación se generan mediante migraciones de Entity Framework.

---

# Ejecutar Update-Database

## Opción recomendada: Package Manager Console

Abrir:

```text
Tools > NuGet Package Manager > Package Manager Console
```

Ejecutar el siguiente comando:

```powershell
Update-Database -Project WBAPI.Infrastructure -StartupProject WBAPI.WebAPI
```

### Descripción

- `-Project` → Proyecto donde se encuentran las migraciones y el DbContext.
- `-StartupProject` → Proyecto principal que contiene la configuración de la aplicación.

---

# Verificación

Después de ejecutar las migraciones:

1. Abrir SQL Server Management Studio.
2. Conectarse a:

```text
(localdb)\MSSQLLocalDB
```

3. Verificar que la base de datos haya sido creada.
4. Confirmar la existencia de tablas:

- AspNetUsers
- AspNetRoles
- AspNetUserRoles
- etc.

---

# Ejecutar el Proyecto

## Desde Visual Studio

1. Establecer `WebAPI` como Startup Project.
2. Presionar:

```text
F5
```

o

```text
Ctrl + F5
```

---

# Swagger

La API tiene Swagger habilitado para pruebas locales.

Una vez iniciada la aplicación, acceder a:

```text
https://localhost:<PUERTO>/swagger
```


---

# Posibles Problemas Comunes

## Error: SDK de .NET no encontrado

Validar instalación ejecutando:

```bash
dotnet --list-sdks
```

---

## Error de conexión a SQL LocalDB

Verificar que exista la instancia:

```bash
sqllocaldb info
```

En caso necesario iniciar la instancia:

```bash
sqllocaldb start MSSQLLocalDB
```

---

## Error al ejecutar migraciones

Verificar:

- Que el proyecto correcto esté seleccionado como Startup Project.
- Que la cadena de conexión sea válida.
- Que el SDK de .NET 10 esté instalado.

---

# Comandos Útiles

## Crear nueva migración

```powershell
Add-Migration NombreMigracion -Project WBAPI.Infrastructure -StartupProject WBAPI.WebAPI
```

## Aplicar migraciones

```powershell
Update-Database -Project WBAPI.Infrastructure -StartupProject WBAPI.WebAPI
```

## Eliminar última migración

```powershell
Remove-Migration -Project WBAPI.Infrastructure -StartupProject WBAPI.WebAPI
```

---

# Arquitectura y Técnicas de Desarrollo Utilizadas

El proyecto fue desarrollado aplicando principios de arquitectura moderna, separación de responsabilidades y buenas prácticas de ingeniería de software orientadas a mantenibilidad, escalabilidad y testabilidad.

## Arquitectura

### Clean Architecture

La solución está estructurada siguiendo el enfoque de Clean Architecture, separando responsabilidades en distintas capas:

- Domain
- Application
- Infrastructure
- Presentation (Web API)

Esto permite:

- Bajo acoplamiento
- Alta cohesión
- Facilidad de mantenimiento
- Escalabilidad
- Mayor facilidad para testing

---

# Patrones y Principios Aplicados

## Dependency Injection (DI)

Se utilizó Inyección de Dependencias para desacoplar componentes y facilitar:

- Testabilidad
- Reemplazo de implementaciones
- Mantenibilidad
- Configuración centralizada de servicios

---

## CQRS (Command Query Responsibility Segregation)

Se implementó CQRS para separar operaciones de lectura y escritura:

- Commands → Operaciones de modificación de estado
- Queries → Operaciones de consulta

Esto permite una mejor organización de responsabilidades y mayor claridad en la lógica de negocio.

---

## Mediator Pattern

Se utilizó el patrón Mediator mediante MediatR para desacoplar controladores de la lógica de aplicación.

Beneficios:

- Reducción de dependencias directas
- Mejor organización del flujo de solicitudes
- Mayor mantenibilidad
- Facilita pipelines y behaviors

---

## Repository Pattern

Se implementó el patrón Repository para abstraer el acceso a datos y desacoplar Entity Framework de la lógica de negocio.

---

## Unit of Work

Se utilizó Unit of Work para centralizar y controlar transacciones y persistencia de cambios en la base de datos.

Beneficios:

- Manejo consistente de transacciones
- Coordinación entre múltiples repositorios
- Mejor control de persistencia

---

## Factory Method Pattern

Se utilizó Factory Method para encapsular la creación de ciertos objetos y reducir el acoplamiento entre implementaciones concretas.

---

# Principios SOLID

Durante el desarrollo se aplicaron principios SOLID, incluyendo:

- Single Responsibility Principle (SRP)
- Open/Closed Principle (OCP)
- Dependency Inversion Principle (DIP)

---

# Seguridad

El proyecto implementa autenticación y autorización utilizando:

- ASP.NET Identity
- JWT (JSON Web Tokens)

---

# Persistencia

Tecnologías utilizadas para acceso a datos:

- Entity Framework Core
- SQL Server LocalDB
- Migrations de EF Core

---

# Otros Enfoques Aplicados

- Separación de responsabilidades
- Programación orientada a interfaces
- Configuración desacoplada
- Manejo centralizado de dependencias
- Arquitectura orientada a mantenibilidad
- Diseño preparado para escalabilidad futura
# Contacto

En caso de problemas durante la configuración o ejecución del proyecto, contactar con Eduardo Barrios (Ingeniero de Software).