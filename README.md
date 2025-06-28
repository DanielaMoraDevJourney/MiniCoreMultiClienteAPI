
# MiniCoreMultiCliente.API

Sistema backend desarrollado con **.NET 8** y **arquitectura limpia**, orientado a la **gestión de ventas, comisiones** y reglas personalizadas para múltiples clientes. Permite administrar vendedores, clientes y calcular automáticamente comisiones aplicando estrategias específicas por cliente.

---

## Arquitectura

Este proyecto aplica **Clean Architecture**, separando claramente responsabilidades por capa:

```

MiniCoreMultiCliente/
│
├── MiniCore.Domain              # Entidades y lógica de negocio pura
├── MiniCore.Application         # Interfaces, DTOs, servicios, estrategias
├── MiniCore.Infrastructure      # Repositorios, EF DbContext, implementación
├── MiniCore.API                 # Controladores REST y configuración

````

---

## Patrón MVC (Adaptado a Clean Architecture)

- **Model (Domain + Application)**: Entidades (`Cliente`, `Vendedor`, `Venta`, `ReglaComision`) y lógica de negocio (`ComisionService`, `ReglaComisionEstandar`).
- **View (Frontend separado)**: Proyecto React conectado vía API REST.
- **Controller (API Controllers)**: Controladores REST que orquestan servicios y devuelven respuestas.

---

## Buenas prácticas aplicadas

### Principios SOLID

1. **Single Responsibility Principle (SRP)**  
   - Cada clase tiene una única responsabilidad:  
     - `ComisionService` calcula comisiones  
     - `ComisionRepository` accede a datos  
     - `ReglaComisionEstandar` aplica lógica de negocio pura

2. **Dependency Inversion Principle (DIP)**  
   - `ComisionService` depende de interfaces (`IComisionRepository`, `IReglaComisionEstrategia`) y no de clases concretas, facilitando pruebas y mantenimiento.

---

### Patrones de Diseño

- **Strategy Pattern**  
  La lógica de cálculo de comisión se encapsula en `ReglaComisionEstandar`, que implementa `IReglaComisionEstrategia`. Permite cambiar fácilmente cómo se calculan las comisiones.

- **Factory Pattern**  
  `ReglaComisionFactory` permite seleccionar dinámicamente la estrategia de cálculo según el contexto del cliente.

---

## Funcionalidades

- Gestión de clientes y vendedores
- Registro de ventas por vendedor
- Reglas de comisión configurables por cliente
- Cálculo automático de comisiones por reglas aplicadas
- Filtro de ventas por fecha
- API REST documentada con Swagger

---

## Tecnologías

- .NET 8
- Entity Framework Core
- SQL Server
- Docker
- Swagger / OpenAPI
- Render.com para despliegue

---

## Configuración

### Variables de entorno

Debes configurar el archivo `appsettings.json` o usar variables del entorno:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=...;User Id=...;Password=...;"
  }
}
````

---

### Ejecución local

```bash
dotnet restore
dotnet build
dotnet run --project MiniCoreMultiCliente.API
```

---

### Ejecución con Docker

```bash
docker build -t minicore-api .
docker run -d -p 5000:80 minicore-api
```

---

## Endpoints principales

| Método | Ruta                     | Descripción                 |
| ------ | ------------------------ | --------------------------- |
| GET    | /api/clientes            | Listar clientes             |
| POST   | /api/clientes            | Crear cliente               |
| GET    | /api/vendedores          | Listar vendedores           |
| POST   | /api/ventas              | Registrar venta             |
| GET    | /api/ventas/por-fechas   | Ventas filtradas por fechas |
| POST   | /api/comisiones/calcular | Calcular comisión           |

---

## Estructura de la API

```bash
MiniCoreMultiCliente.API
├── Controllers/
│   ├── ClientesController.cs
│   ├── VendedoresController.cs
│   ├── VentasController.cs
│   └── ComisionesController.cs
├── Program.cs
├── appsettings.json
```

---

## Mantenimiento y mejoras futuras

* Soporte a reglas por tipo de producto o metas
* Autenticación y autorización basada en roles
* Integración con sistemas externos (ej. webhook de pagos)
* Multitenancy completo a nivel de base de datos

---

## Proyecto Frontend

El frontend React que consume esta API se encuentra en:

* **Repositorio GitHub:**
  [MiniCoreMultiCliente.FrontEnd](https://github.com/DanielaMoraDevJourney/MiniCoreMultiCliente.FrontEnd.git)

* **Deploy en Render:**
  [https://minicoremulticliente-frontend.onrender.com](https://minicoremulticliente-frontend.onrender.com)

---

## Autor

Desarrollado por [Daniela Mora](https://github.com/DanielaMoraDevJourney) como proyecto demostrativo de arquitectura limpia con integración frontend-backend, principios SOLID y patrones de diseño aplicados correctamente.


---

