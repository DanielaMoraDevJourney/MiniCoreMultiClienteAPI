
# MiniCoreMultiCliente.API

Sistema backend en .NET 8 con arquitectura limpia, orientado a la gestión de ventas, comisiones y reglas personalizadas para múltiples clientes. Este sistema permite administrar vendedores, ventas y comisiones aplicando reglas diferenciadas según el cliente.

## Arquitectura

Este backend sigue el patrón **Clean Architecture**, estructurado en capas:

```

MiniCoreMultiCliente.API/
│
├── MiniCore.Application         # Lógica de negocio (casos de uso, interfaces de servicio)
├── MiniCore.Domain              # Entidades y modelos de dominio
├── MiniCore.Infrastructure      # Implementación de servicios, acceso a datos (DbContext)
├── MiniCore.API                 # Capa de presentación y controladores

````

### Patrón MVC adaptado a Clean Architecture

- **Model (Domain + Application)**: Entidades (`Cliente`, `Vendedor`, `Venta`, `ReglaComision`) y lógica de negocio en `Application`.
- **View (Frontend separado)**: Proyecto React independiente conectado vía API REST.
- **Controller (API Controllers)**: En `MiniCore.API`, los controladores exponen los endpoints al frontend y orquestan servicios.

## Funcionalidades

- Gestión de clientes y vendedores.
- Registro de ventas por vendedor.
- Reglas de comisión configurables por cliente.
- Cálculo automático de comisiones según reglas vigentes.
- Filtro de ventas por fechas.
- API REST documentada con Swagger.

## Tecnologías

- **.NET 8**
- **Entity Framework Core**
- **SQL Server**
- **Docker**
- **Swagger**
- **Render.com** (despliegue en la nube)

## Configuración

### Variables de entorno requeridas

Asegúrate de definir estas variables en `appsettings.json` o entorno:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=...;User Id=...;Password=...;"
  }
}
````

### Ejecución local

```bash
# Restaura paquetes y compila
dotnet restore
dotnet build

# Ejecuta la API
dotnet run --project MiniCoreMultiCliente.API
```

### Ejecución con Docker

```bash
# Build de la imagen
docker build -t minicore-api .

# Run en localhost
docker run -d -p 5000:80 minicore-api
```

## Endpoints principales

| Método | Ruta                        | Descripción                    |
| ------ | --------------------------- | ------------------------------ |
| GET    | /api/clientes               | Listar clientes                |
| GET    | /api/vendedores             | Listar vendedores              |
| POST   | /api/ventas                 | Crear nueva venta              |
| GET    | /api/ventas/por-fechas      | Filtrar ventas por fechas      |
| GET    | /api/comisiones/{clienteId} | Obtener comisiones por cliente |

## Estructura de carpetas

```bash
MiniCoreMultiCliente.API
├── Controllers
│   └── ClientesController.cs
│   └── VendedoresController.cs
│   └── VentasController.cs
│   └── ComisionesController.cs
├── Program.cs
├── appsettings.json
```

## Mantenimiento y mejoras

* Se pueden extender las reglas de comisión por tipo de producto o meta mensual.
* Puede integrarse autenticación y autorización por roles.
* Permite integración con sistemas externos vía webhooks.

## Proyecto Frontend

El frontend React se encuentra en el repositorio:

[MiniCoreMultiCliente.FrontEnd](https://github.com/DanielaMoraDevJourney/MiniCoreMultiCliente.FrontEnd.git)
[Deploy](https://minicoremulticliente-frontend.onrender.com)

---

## Autor

Desarrollado por [Daniela Mora](https://github.com/DanielaMoraDevJourney) como proyecto demostrativo de arquitectura limpia con integración frontend-backend.

